using Azure.CosmosDB.GeoApi.Entities;
using System;

namespace Azure.CosmosDB.GeoApi.Harness
{
    class Program
    {
        private static CustomerService _customerService;

        static void Main(string[] args)
        {
            _customerService = new CustomerService(new CosmosDbBaseRepository());

            var id = _customerService.CreateCustomer(Customer.Mock());

            var person = _customerService.GetCustomer(id);

            Console.WriteLine(person.FName);
            Console.ReadLine();
        }
    }
}
