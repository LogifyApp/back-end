using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Data.DTOs.AttachmentsDTOs;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogifyBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttachmentController(IAttachmentService attachmentService) : ControllerBase
{
    [HttpPost("add")]
    public async Task<IActionResult> AddAttachment([FromBody] CreateAttachmentDto dto)
    {
        var attachment = await attachmentService.AddAttachment(dto);
        return Ok(attachment);
    }

    [HttpDelete("{attachmentId}")]
    public async Task<IActionResult> DeleteAttachment(int attachmentId)
    {
        var success = await attachmentService.DeleteAttachment(attachmentId);
        return success ? Ok("Attachment deleted") : NotFound("Attachment not found");
    }
}