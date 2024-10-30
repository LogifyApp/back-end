using LogifyBackEnd.Data.DTOs;

namespace LogifyBackEnd.Services.Interfaces;

public interface IDocumentService
{
    Task<DocumentDto> UploadDocument(UploadDocumentDto uploadDocumentDto);
    Task<bool> AttachDocumentToCargo(int cargoId, int documentId);
    Task<bool> DeleteDocument(int documentId);
    Task<DocumentDto?> RetrieveDocument(int documentId);
}