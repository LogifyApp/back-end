using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Data.DTOs.MessagesDTOs;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogifyBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController(IMessageService messageService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateMessage([FromBody] CreateMessageDto dto)
    {
        var message = await messageService.CreateMessage(dto);
        return Ok(message);
    }

    [HttpDelete("{messageId}")]
    public async Task<IActionResult> DeleteMessage(int messageId)
    {
        var success = await messageService.DeleteMessage(messageId);
        return success ? Ok("Message deleted") : NotFound("Message not found");
    }
    
    [HttpPut("{messageId}/update")]
    public async Task<IActionResult> UpdateMessageContent(int messageId, [FromBody] UpdateMessageDto updateMessageDto)
    {
        var success = await messageService.UpdateMessageContent(messageId, updateMessageDto);
        return success ? Ok("Message updated") : NotFound("Message not found");
    }
}