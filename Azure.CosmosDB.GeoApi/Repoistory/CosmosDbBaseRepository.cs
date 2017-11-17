using Azure.CosmosDB.GeoApi.Helpers;
using Azure.CosmosDB.GeoApi.Repository.Interfaces;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Azure.CosmosDB.GeoApi.Repository
{
    public class CosmosDbBaseRepository : ICosmosDbBaseRepository
    {
        private readonly string _accountKey;
        private readonly Uri _accountEndpoint;

        public CosmosDbBaseRepository()
        {
            _accountEndpoint = new Uri(ConfigurationManager.AppSettings["CosmosDb:Endpoint"]);
            _accountKey = ConfigurationManager.AppSettings["CosmosDb:Key"];
        }

        public async Task<DocumentResponse<T>> ReadAsync<T>(string id, string dbId, string collectionId)
        {
            var uri = UriFactory.CreateDocumentUri(dbId, collectionId, id);

            return await GetClient().ReadDocumentAsync<T>(uri, new RequestOptions { PartitionKey = new PartitionKey(id) });
        }

        public async Task<ResourceResponse<Document>> WriteAsync<T>(T data, string dbId, string collectionId)
        {
            var uri = UriFactory.CreateDocumentCollectionUri(dbId, collectionId);

            return await GetClient().CreateDocumentAsync(uri, data);
        }

        public async Task<ResourceResponse<Document>> UpsertAsync<T>(T data, string dbId, string collectionId)
        {
            var uri = UriFactory.CreateDocumentCollectionUri(dbId, collectionId);

            return await GetClient().UpsertDocumentAsync(uri, data);
        }

        public async Task<ResourceResponse<Document>> DeleteAsync(string dbId, string collectionId)
        {
            var uri = UriFactory.CreateDocumentCollectionUri(dbId, collectionId);

            return await GetClient().DeleteDocumentAsync(uri);
        }

        private DocumentClient GetClient()
        {
            return new DocumentClient(_accountEndpoint, _accountKey, CosmosDbHelper.GetPolicy());
        }
    }
}
