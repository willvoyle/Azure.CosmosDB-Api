using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Azure.CosmosDB.GeoApi.DTOs
{
    public class DocDbRepsonseHolder<T>
    {
        public T Data { get; set; }
        public ActionResponse Response { get; set; }
        public string Message { get; set; }
        public bool IsSuccess => (Response == ActionResponse.Ok || Response == ActionResponse.Created);
    }

    public enum ActionResponse
    {
        Ok = 200,
        Created = 201,
        NotFound = 404,
        InternalServerError = 500
    }
}