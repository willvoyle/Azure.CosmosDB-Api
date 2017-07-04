using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Collections.Generic;

namespace Azure.CosmosDB.GeoApi.Helpers
{
    public static class CosmosDbHelper
    {
        public static ConnectionPolicy GetPolicy()
        {
            var policy = new ConnectionPolicy();

            policy.PreferredLocations.Add(LocationNames.UKSouth);
            policy.PreferredLocations.Add(LocationNames.UKWest);
            policy.PreferredLocations.Add(LocationNames.NorthEurope);

            return policy;
        }

        public static ConnectionPolicy GetPolicy(string locale)
        {
            var policy = new ConnectionPolicy();

            GetNames(locale).ForEach(n => policy.PreferredLocations.Add(n));

            return policy;
        }

        private static List<string> GetNames(string locale)
        {
            switch (locale)
            {
                case "en-GB":
                    return new List<string> { { LocationNames.UKSouth }, { LocationNames.UKWest }, { LocationNames.NorthEurope } };
                case "fr-FR":
                    return new List<string> { { LocationNames.NorthEurope }, { LocationNames.UKSouth } };
                default:
                    return default(List<string>);
            }
        }
    }
}
