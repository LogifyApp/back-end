using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogifyBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var user = await userService.Register(registerDto);
        if (user == null)
            return BadRequest("Registration failed");

        return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var token = await userService.Login(loginDto);
        if (string.IsNullOrEmpty(token))
            return Unauthorized();

        return Ok(new { Token = token });
    }
}