using LogifyBackEnd.Data;
using LogifyBackEnd.Data.DTOs;
using LogifyBackEnd.Data.DTOs.DocumentsDTOs;
using LogifyBackEnd.Models;
using LogifyBackEnd.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LogifyBackEnd.Services;

public class DocumentService(DBContext context, IMongoDatabase mongoDatabase) : IDocumentService
{
    private readonly IMongoCollection<BsonDocument> _mongoCollection = mongoDatabase.GetCollection<BsonDocument>("documents");

        public async Task<DocumentDto> UploadDocument(UploadDocumentDto uploadDocumentDto)
        {
            // Step 1: Save file to MongoDB
            var mongoDocument = new BsonDocument
            {
                { "filename", uploadDocumentDto.File.FileName },
                { "filetype", uploadDocumentDto.File.ContentType },
                { "fileURL", $"https://example.com/path/to/{uploadDocumentDto.File.FileName}" },  // Optional file URL generator
                { "content", await GetFileBytes(uploadDocumentDto.File) },
                { "uploaded_at", DateTime.UtcNow }
            };
            await _mongoCollection.InsertOneAsync(mongoDocument);

            // Step 2: Insert metadata into SQL Server
            var sqlDocument = new Document
            {
                Filename = uploadDocumentDto.File.FileName,
                Filetype = uploadDocumentDto.File.ContentType,
                MongoId = mongoDocument["_id"].AsObjectId.ToString(),  // Store MongoId as hash or string representation
                FileUrl = mongoDocument["fileURL"].AsString
            };
            context.Documents.Add(sqlDocument);
            await context.SaveChangesAsync();

            return new DocumentDto
            {
                Id = sqlDocument.Id,
                Filename = sqlDocument.Filename,
                Filetype = sqlDocument.Filetype,
                FileUrl = sqlDocument.FileUrl,
                UploadedAt = mongoDocument["uploaded_at"].ToUniversalTime()
            };
        }

        public async Task<bool> AttachDocumentToCargo(int cargoId, int documentId)
        {
            var cargoDocument = new CargoDocument
            {
                CargoId = cargoId,
                DocumentId = documentId
            };
            context.CargoDocuments.Add(cargoDocument);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDocument(int documentId)
        {
            var document = await context.Documents.FindAsync(documentId);
            if (document == null) return false;

            // Delete from MongoDB
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(document.MongoId.ToString()));
            await _mongoCollection.DeleteOneAsync(filter);

            // Delete from SQL Server
            context.Documents.Remove(document);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<DocumentDto?> RetrieveDocument(int documentId)
        {
            var document = await context.Documents.FindAsync(documentId);
            if (document == null) return null;

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(document.MongoId));
            var mongoDocument = await _mongoCollection.Find(filter).FirstOrDefaultAsync();
            if (mongoDocument == null) return null;

            return new DocumentDto
            {
                Id = document.Id,
                Filename = mongoDocument["filename"].AsString,
                Filetype = mongoDocument["filetype"].AsString,
                FileUrl = mongoDocument["fileURL"].AsString,
                Content = mongoDocument["content"].AsByteArray,
                UploadedAt = mongoDocument["uploaded_at"].ToUniversalTime()
            };
        }

        private async Task<byte[]> GetFileBytes(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }
    }