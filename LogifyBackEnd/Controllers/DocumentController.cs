using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Data.DTOs.DocumentsDTOs;
using LogifyBackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogifyBackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentController(IDocumentService documentService) : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> UploadDocument([FromForm] UploadDocumentDto dto)
    {
        var document = await documentService.UploadDocument(dto);
        return Ok(document);
    }

    [HttpPost("{cargoId}/attach/{documentId}")]
    public async Task<IActionResult> AttachDocumentToCargo(int cargoId, int documentId)
    {
        var success = await documentService.AttachDocumentToCargo(cargoId, documentId);
        return success ? Ok("Document attached to cargo") : NotFound("Cargo or Document not found");
    }

    [HttpDelete("{documentId}")]
    public async Task<IActionResult> DeleteDocument(int documentId)
    {
        var success = await documentService.DeleteDocument(documentId);
        return success ? Ok("Document deleted") : NotFound("Document not found");
    }

    [HttpGet("{documentId}")]
    public async Task<IActionResult> RetrieveDocument(int documentId)
    {
        var document = await documentService.RetrieveDocument(documentId);
        return document != null ? File(document.Content, document.Filetype, document.Filename) : NotFound("Document not found");
    }
}