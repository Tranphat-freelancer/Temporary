using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SER.Infrastructure.Data;

public interface IGenericRepository<TEntity, TContext>
    where TEntity : class
    where TContext : DbContext
{
    List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null!, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!, string includeProperties = "", int PageIndex = 0, int PageSize = 0);
    IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> filter = null!, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!, string includeProperties = "");
    TEntity GetByID(object id);
    void Insert(TEntity entity);
    void InsertRange(IEnumerable<TEntity> entities);
    void TryDelete(object id);
    void Delete(TEntity entity);
    public void DeleteRange(IEnumerable<TEntity> entities);

    void Update(TEntity entityToUpdate);
}
