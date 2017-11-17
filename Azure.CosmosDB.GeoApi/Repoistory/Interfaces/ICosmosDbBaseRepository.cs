using Azure.CosmosDB.GeoApi.DTOs;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;

namespace Azure.CosmosDB.GeoApi.Repository.Interfaces
{
    public interface ICosmosDbBaseRepository
    {
        Task<DocDbRepsonseHolder<DocumentResponse<T>>> ReadAsync<T>(string id, string dbId, string collectionId);
        Task<DocDbRepsonseHolder<Document>> WriteAsync<T>(T data, string dbId, string collectionId);
        Task<DocDbRepsonseHolder<Document>> DeleteAsync(string dbId, string collectionId);
        Task<DocDbRepsonseHolder<Document>> UpsertAsync<T>(T data, string dbId, string collectionId);
    }
}