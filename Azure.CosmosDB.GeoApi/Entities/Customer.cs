using Newtonsoft.Json;
using System;
using System.Globalization;

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

        public static Customer Mock()
        {
            var rnd = new Random();
            return new Customer { FName = $"FTest-{rnd.Next()}", LName = $"LTest-{rnd.Next()}", Locale = CultureInfo.CurrentCulture.Name };
        }
    }
}
