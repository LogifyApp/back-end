using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogifyBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateChat([FromBody] CreateChatDto dto)
    {
        try
        {
            var chat = await _chatService.CreateChat(dto.EmployerUserId, dto.DriverUserId);
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
        var success = await _chatService.DeleteChat(chatId);
        return success ? Ok("Chat deleted") : NotFound("Chat not found");
    }
}