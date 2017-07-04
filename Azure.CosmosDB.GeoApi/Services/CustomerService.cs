using Azure.CosmosDB.GeoApi.Entities;
using System;

namespace Azure.CosmosDB.GeoApi
{
    public class CustomerService
    {
        private readonly CosmosDbBaseRepository _cosmosDbRepo;

        public CustomerService(CosmosDbBaseRepository cosmosDbRepo)
        {
            _cosmosDbRepo = cosmosDbRepo;
        }

        public Guid CreateCustomer(Customer mockPerson)
        {
            mockPerson.Id = Guid.NewGuid();

            var response = _cosmosDbRepo.WriteAsync<Customer>(mockPerson, "Db", "Customer").Result;

            return Guid.Parse(response.Resource.Id);
        }

        public Customer GetCustomer(Guid id)
        {
            var person = _cosmosDbRepo.ReadAsync<Customer>(id.ToString(), "Db", "Customer");

            return person.Result.Document;
        }
    }
}
