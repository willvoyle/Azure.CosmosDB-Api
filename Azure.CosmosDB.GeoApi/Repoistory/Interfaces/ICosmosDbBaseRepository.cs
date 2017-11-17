using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;

namespace Azure.CosmosDB.GeoApi.Repository.Interfaces
{
    public interface ICosmosDbBaseRepository
    {
        Task<DocumentResponse<T>> ReadAsync<T>(string id, string dbId, string collectionId);
        Task<ResourceResponse<Document>> WriteAsync<T>(T data, string dbId, string collectionId);
        Task<ResourceResponse<Document>> DeleteAsync(string dbId, string collectionId);
        Task<ResourceResponse<Document>> UpsertAsync<T>(T data, string dbId, string collectionId);
    }
}