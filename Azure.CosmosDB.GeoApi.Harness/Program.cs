using Azure.CosmosDB.GeoApi.Entities;
using System;
using System.Globalization;

namespace Azure.CosmosDB.GeoApi.Harness
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new CustomerService(new CosmosDbBaseRepository());

            var mockPerson = new Customer { FName = "FTest", LName = "LTest", Locale = CultureInfo.CurrentCulture.Name };

            var id = service.CreateCustomer(mockPerson);

            var person = service.GetCustomer(id);

            Console.WriteLine(person.FName);
            Console.ReadLine();
        }
    }
}
