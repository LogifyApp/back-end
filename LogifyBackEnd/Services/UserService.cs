using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LogifyBackEnd.Data;
using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Models;
using LogifyBackEnd.Models.Enums;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LogifyBackEnd.Services;

public class UserService : IUserService
    {
        private readonly DBContext _context;
        private readonly IConfiguration _configuration;

        public UserService(DBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User> Register(RegisterDto registerDto)
        {
            // Hash the password
            var hashedPassword = HashPassword(registerDto.Password);

            // Create User with hashed password
            var user = new User
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                PhoneNumber = registerDto.PhoneNumber,
                PasswordHash = hashedPassword,
                Role = registerDto.Role
            };

            // Check the role and assign the corresponding entity
            if (registerDto.Role.Equals("driver", StringComparison.OrdinalIgnoreCase))
            {
                user.Driver = new Driver
                {
                    Status = DriverStatus.WithoutEmp
                };
            }
            else if (registerDto.Role.Equals("employer", StringComparison.OrdinalIgnoreCase))
            {
                user.Employer = new Employer();
            }

            // Add the user to the context, EF will handle setting the ID for related entities
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }


        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == loginDto.PhoneNumber);

            if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                return null;
            }

            return CreateJwtToken(user);
        }

        private string CreateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }