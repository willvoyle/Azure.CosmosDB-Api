using Azure.CosmosDB.GeoApi.Entities;
using Azure.CosmosDB.GeoApi.Repository.Interfaces;
using Azure.CosmosDB.GeoApi.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Azure.CosmosDB.GeoApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICosmosDbBaseRepository _cosmosDbRepo;

        public CustomerService(ICosmosDbBaseRepository cosmosDbRepo)
        {
            _cosmosDbRepo = cosmosDbRepo;
        }

        public async Task<Guid> CreateCustomerAsync(Customer mockPerson)
        {
            mockPerson.Id = Guid.NewGuid();

            var response = await _cosmosDbRepo.WriteAsync<Customer>(mockPerson, "Db", "Customer");

            return Guid.Parse(response.Resource.Id);
        }

        public async Task<Customer> GetCustomerAsync(Guid id)
        {
            var person = await _cosmosDbRepo.ReadAsync<Customer>(id.ToString(), "Db", "Customer");

            return person.Document;
        }
    }
}
