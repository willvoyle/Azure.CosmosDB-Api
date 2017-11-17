using Azure.CosmosDB.GeoApi.DTOs;
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

        public async Task<DocDbRepsonseHolder<DocumentResponse<T>>> ReadAsync<T>(string id, string dbId, string collectionId)
        {
            var uri = UriFactory.CreateDocumentUri(dbId, collectionId, id);

            try
            {
                var response = await GetClient().ReadDocumentAsync<T>(uri, new RequestOptions { PartitionKey = new PartitionKey(id) });
                return new DocDbRepsonseHolder<DocumentResponse<T>> { Data = response, Response = ActionResponse.Ok, Message = $"DocDb: Docuemnt read - {uri}" };
            }
            catch (Exception ex)
            {
                //Log error
                return new DocDbRepsonseHolder<DocumentResponse<T>> { Response = ActionResponse.InternalServerError, Message = $"DocDb: Unable to reader Doucment - {uri} - {ex.Message}" };
            }


        }

        public async Task<DocDbRepsonseHolder<Document>> WriteAsync<T>(T data, string dbId, string collectionId)
        {
            var uri = UriFactory.CreateDocumentCollectionUri(dbId, collectionId);

            try
            {
                var response = await GetClient().CreateDocumentAsync(uri, data);
                return new DocDbRepsonseHolder<Document> { Data = response.Resource, Response = ActionResponse.Created, Message = $"DocDb: Docuemnt created - {uri}" };
            }
            catch (Exception ex)
            {
                //Log error
                return new DocDbRepsonseHolder<Document> { Response = ActionResponse.InternalServerError, Message = $"DocDb: Unable to create Doucment - {uri} - {ex.Message}" };
            }

        }

        public async Task<DocDbRepsonseHolder<Document>> UpsertAsync<T>(T data, string dbId, string collectionId)
        {
            var uri = UriFactory.CreateDocumentCollectionUri(dbId, collectionId);

            try
            {
                var response = await GetClient().UpsertDocumentAsync(uri, data);
                return new DocDbRepsonseHolder<Document> { Data = response.Resource, Response = ActionResponse.Created, Message = $"DocDb: Document upserted - {uri}" };
            }
            catch (Exception ex)
            {
                //Log error
                return new DocDbRepsonseHolder<Document> { Response = ActionResponse.InternalServerError, Message = $"DocDb: Unable to upsert Doucment - {uri} - {ex.Message}" };
            }

        }

        public async Task<DocDbRepsonseHolder<Document>> DeleteAsync(string dbId, string collectionId)
        {
            var uri = UriFactory.CreateDocumentCollectionUri(dbId, collectionId);

            try
            {
                var response = await GetClient().DeleteDocumentAsync(uri);
                return new DocDbRepsonseHolder<Document> { Response = ActionResponse.Ok, Message = $"DocDb: Document deleted - {uri}" };
            }
            catch (Exception ex)
            {
                //Log error
                return new DocDbRepsonseHolder<Document> { Response = ActionResponse.InternalServerError, Message = $"DocDb: Unable to delete Doucment - {uri} - {ex.Message}" };
            }
        }

        private DocumentClient GetClient()
        {
            return new DocumentClient(_accountEndpoint, _accountKey, CosmosDbHelper.GetPolicy());
        }
    }
}
