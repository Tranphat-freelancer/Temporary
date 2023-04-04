using SER.DataAccess;
using SER.Domain.Entities;

namespace SER.Infrastructure.Data.ProductRepository;

public interface IProductRepository : IGenericRepository<Product, ApplicationDbContext>
{
}
