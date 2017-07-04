using Azure.CosmosDB.GeoApi.Helpers;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Azure.CosmosDB.GeoApi
{
    public class CosmosDbBaseRepository
    {
        private readonly string _accountKey;
        private readonly Uri _accountEndpoint;

        public CosmosDbBaseRepository()
        {
            _accountEndpoint = new Uri(ConfigurationManager.AppSettings["CosmosDb:Endpoint"]);
            _accountKey = ConfigurationManager.AppSettings["CosmosDb:Key"];
        }

        public Task<DocumentResponse<T>> ReadAsync<T>(string id, string dbId, string collectionId)
        {
            var uri = UriFactory.CreateDocumentUri(dbId, collectionId, id);

            return GetClient().ReadDocumentAsync<T>(uri, new RequestOptions { PartitionKey = new PartitionKey(id) });
        }

        public Task<ResourceResponse<Document>> WriteAsync<T>(T data, string dbId, string collectionId)
        {
            var uri = UriFactory.CreateDocumentCollectionUri(dbId, collectionId);

            return GetClient().CreateDocumentAsync(uri, data);
        }

        private DocumentClient GetClient()
        {
            return new DocumentClient(_accountEndpoint, _accountKey, CosmosDbHelper.GetPolicy());
        }
    }
}
