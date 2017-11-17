using Azure.CosmosDB.GeoApi.Entities;
using Azure.CosmosDB.GeoApi.Repository;
using Azure.CosmosDB.GeoApi.Services;
using Azure.CosmosDB.GeoApi.Services.Interfaces;
using System;

namespace Azure.CosmosDB.GeoApi.Harness
{
    class Program
    {
        private static ICustomerService _customerService;

        static void Main(string[] args)
        {
            _customerService = new CustomerService(new CosmosDbBaseRepository());

            CreateCustMock();
        }

        private static async void CreateCustMock()
        {
            var id = await _customerService.CreateCustomerAsync(Customer.Mock());

            var person = await _customerService.GetCustomerAsync(id);

            Console.WriteLine(person.FName);
            Console.ReadLine();
        }
    }
}
