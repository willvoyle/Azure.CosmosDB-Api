using Azure.CosmosDB.GeoApi.Entities;
using System;

namespace Azure.CosmosDB.GeoApi.Harness
{
    class Program
    {
        private static CustomerService _customerService;

        public Program()
        {
            _customerService = new CustomerService(new CosmosDbBaseRepository());
        }

        static void Main(string[] args)
        {
            var id = _customerService.CreateCustomer(Customer.Mock());

            var person = _customerService.GetCustomer(id);

            Console.WriteLine(person.FName);
            Console.ReadLine();
        }
    }
}
