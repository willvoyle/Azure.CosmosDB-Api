using Newtonsoft.Json;
using System;

namespace Azure.CosmosDB.GeoApi.Entities
{
    public class Customer
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("fName")]
        public string FName { get; set; }
        [JsonProperty("lName")]
        public string LName { get; set; }
        [JsonProperty("locale")]
        public string Locale { get; set; }
    }
}
