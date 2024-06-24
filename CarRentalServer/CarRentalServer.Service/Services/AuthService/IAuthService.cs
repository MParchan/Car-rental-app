using CarRentalServer.Repository.Entities;
using CarRentalServer.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServer.Service.Services.AuthService
{
    public interface IAuthService
    {
        Task Register(string email, string password, string confirmPassword);
        Task<string> Login(string email, string password);
        Task<(string Email, string Password)> CreateManager(string email, string password, string confirmPassword);
    }
}
