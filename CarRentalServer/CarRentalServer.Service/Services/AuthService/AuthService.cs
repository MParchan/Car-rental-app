using AutoMapper;
using CarRentalServer.Repository.Entities;
using CarRentalServer.Repository.Repositories.RoleRepository;
using CarRentalServer.Repository.Repositories.UserRepository;
using CarRentalServer.Service.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthService(IUserRepository userRepository, IRoleRepository roleRepository, IConfiguration config, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _config = config;
            _mapper = mapper;
        }


        public async Task Register(string email, string password, string confirmPassword)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(email);
            if (existingUser != null)
            {
                throw new ValidationException("Email already in use.");
            }
            if (password.Length < 8)
            {
                throw new ValidationException("Password must be at least 8 characters long.");
            }
            if (!password.Equals(confirmPassword))
            {
                throw new ValidationException("Password and confirm password is not the same.");
            }

            int roleId = await _roleRepository.GetRoleIdByNameAsync("User");
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            UserDto user = new() { RoleId = roleId, Email = email, PasswordHash = passwordHash, PasswordSalt = passwordSalt };
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(user, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            await _userRepository.AddAsync(_mapper.Map<User>(user));
        }

        public async Task<string> Login(string email, string password)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(email);
            if (existingUser == null || !VerifyPasswordHash(password, existingUser.PasswordHash, existingUser.PasswordSalt))
            {
                throw new ValidationException("Email or password is not valid.");
            }

            return CreateToken(email);
        }


        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        private string CreateToken(string email)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, email)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var jwt = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
    }
}
