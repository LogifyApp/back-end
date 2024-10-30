using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Data.DTOs.DocumentsDTOs;

namespace LogifyBackEnd.Services.Interfaces;

public interface IDocumentService
{
    Task<DocumentDto> UploadDocument(UploadDocumentDto uploadDocumentDto);
    Task<bool> AttachDocumentToCargo(int cargoId, int documentId);
    Task<bool> DeleteDocument(int documentId);
    Task<DocumentDto?> RetrieveDocument(int documentId);
}