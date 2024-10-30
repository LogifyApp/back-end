using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogifyBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController(IChatService chatService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateChat([FromBody] CreateChatDto dto)
    {
        try
        {
            var chat = await chatService.CreateChat(dto.EmployerUserId, dto.DriverUserId);
            return Ok(chat);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{chatId}")]
    public async Task<IActionResult> DeleteChat(int chatId)
    {
        var success = await chatService.DeleteChat(chatId);
        return success ? Ok("Chat deleted") : NotFound("Chat not found");
    }
}