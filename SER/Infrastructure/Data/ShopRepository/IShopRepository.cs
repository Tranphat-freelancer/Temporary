using SER.DataAccess;
using SER.Domain.Entities;

namespace SER.Infrastructure.Data.ShopRepository;

public interface IShopRepository : IGenericRepository<Shop, ApplicationDbContext>
{
}
