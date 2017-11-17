using Azure.CosmosDB.GeoApi.Entities;
using System;
using System.Threading.Tasks;

namespace Azure.CosmosDB.GeoApi.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Guid> CreateCustomerAsync(Customer mockPerson);
        Task<Customer> GetCustomerAsync(Guid id);
    }
}