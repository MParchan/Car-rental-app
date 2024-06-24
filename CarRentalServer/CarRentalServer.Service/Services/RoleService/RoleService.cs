using AutoMapper;
using CarRentalServer.Repository.Entities;
using CarRentalServer.Repository.Repositories.RoleRepository;
using CarRentalServer.Service.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            return _mapper.Map<List<RoleDto>>(await _roleRepository.GetAllAsync());
        }

        public async Task<RoleDto> GetRoleByIdAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
            {
                throw new KeyNotFoundException("Role not found.");
            }
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> AddRoleAsync(RoleDto role)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(role, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(role, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            var roleEntity = _mapper.Map<Role>(role);
            await _roleRepository.AddAsync(roleEntity);
            return await GetRoleByIdAsync(roleEntity.RoleId);
        }

        public async Task UpdateRoleAsync(RoleDto role)
        {
            var existingRole = await _roleRepository.GetByIdAsync(role.RoleId);
            if (existingRole == null)
            {
                throw new KeyNotFoundException("Role not found.");
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(role, serviceProvider: null, items: null);
            if (!Validator.TryValidateObject(role, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            await _roleRepository.UpdateAsync(_mapper.Map(role, existingRole));
        }

        public async Task DeleteRoleAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
            {
                throw new KeyNotFoundException("Role not found.");
            }
            await _roleRepository.DeleteAsync(role);
        }
    }
}
