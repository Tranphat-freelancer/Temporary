using SER.DataAccess;
using SER.Domain.Entities;

namespace SER.Infrastructure.Data.ShopRepository;

public class ShopRepository : GenericRepository<Shop, ApplicationDbContext>, IShopRepository
{
    public ShopRepository(ApplicationDbContext context) : base(context)
    {

    }
}
