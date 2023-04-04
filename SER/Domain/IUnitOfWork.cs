using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SER.DataAccess;
using SER.Infrastructure.Data.CustomerRespository;
using SER.Infrastructure.Data.ProductRepository;
using SER.Infrastructure.Data.ShopRepository;

namespace SER.Domain;

public interface IUnitOfWork : IDisposable
{


    ICustomerRepository Customer { get; }
    IShopRepository Shop { get; }
    IProductRepository Product { get; }

    public Task CommitAsync();
    public void Commit();
    public void CommitTransaction<TContext>() where TContext : DbContext;
    public void RollbackTransaction<TContext>() where TContext : DbContext;
    public void BeginTransaction<TContext>() where TContext : DbContext;
}
public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;
    private IConfiguration _config;
    public UnitOfWork(IConfiguration config)
    {
        _config = config;
        _context = new ApplicationDbContext(_config);
    }

    /// <summary>
    /// Setter injection
    /// </summary>
    #region SqlServer Repositories
    private ICustomerRepository? CustomerRepository { get; }
    public ICustomerRepository Customer => CustomerRepository ?? new CustomerRepository(_context);
    private IProductRepository? ProductRepository { get; }
    public IProductRepository Product => ProductRepository ?? new ProductRepository(_context);
    private IShopRepository? ShopRepository { get; }
    public IShopRepository Shop => ShopRepository ?? new ShopRepository(_context);
    #endregion
    private bool disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
                _transaction?.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Commit()
    {
        _context.SaveChanges();
    }
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
    public void BeginTransaction<TContext>() where TContext : DbContext
    {
        if (typeof(TContext) == typeof(ApplicationDbContext))
            _transaction = _context.Database.BeginTransaction();
    }

    public void CommitTransaction<TContext>() where TContext : DbContext => _transaction?.Commit();

    public void RollbackTransaction<TContext>() where TContext : DbContext => _transaction?.Rollback();

}
