using SER.DataAccess;
using SER.Domain.Entities;

namespace SER.Infrastructure.Data.CustomerRespository;

public interface ICustomerRepository : IGenericRepository<Customer, ApplicationDbContext>
{
}
