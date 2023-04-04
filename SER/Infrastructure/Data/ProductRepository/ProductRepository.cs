using SER.DataAccess;
using SER.Domain.Entities;

namespace SER.Infrastructure.Data.ProductRepository;

public class ProductRepository : GenericRepository<Product, ApplicationDbContext>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {

    }
}
