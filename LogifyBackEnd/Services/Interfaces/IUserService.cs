using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Models;

namespace LogifyBackEnd.Services.Interfaces;

public interface IUserService
{
    Task<User> Register(RegisterDto registerDto);
    Task<string> Login(LoginDto loginDto);
}