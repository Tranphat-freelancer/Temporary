using SER.DataAccess;
using SER.Domain.Entities;

namespace SER.Infrastructure.Data.CustomerRespository;

public class CustomerRepository : GenericRepository<Customer, ApplicationDbContext>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
    }
}
