using SER.Domain.Entities;

namespace SER.Domain.Services;

public interface IProductService
{
    object? GetAll();
    object? GetPaged(int pageIndex, int pageSize);
    object? GetEntity(object id);
    object? CreateEntity(Product request);
    object? UpdateEntity(Product request);
    object? DeleteEntity(object id);
}
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    public ProductService(IUnitOfWork unitOfWork, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public object? CreateEntity(Product request)
    {
        try
        {
            //mapper nếu dùng auto mapper
            //...
            _unitOfWork.Product.Insert(request);
            _unitOfWork.Commit();

            return request;
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message, "some err");
            throw;
        }
    }

    public object? DeleteEntity(object id)
    {
        try
        {
            var entity = _unitOfWork.Product.GetByID(id);
            if (entity == null) return entity;

            _unitOfWork.Product.Delete(entity);
            _unitOfWork.Commit();

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message, "some err");
            throw;
        }
    }

    public object? GetAll()
    {
        try
        {
            //mapper nếu dùng auto mapper
            //...

            return _unitOfWork.Product.GetList(orderBy: e => e.OrderByDescending(s => s.Price)); ;
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message, "some err");
            throw;
        }
    }

    public object? GetEntity(object id)
    {
        try
        {
            return _unitOfWork.Product.GetByID(id);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message, "some err");
            throw;
        }
    }

    public object? GetPaged(int pageIndex, int pageSize)
    {
        try
        {
            //mapper nếu dùng auto mapper
            //...
            if (pageIndex > 0 && pageSize > 0)
                return _unitOfWork.Product.GetQuery(orderBy: e => e.OrderByDescending(s => s.Price)).Skip(pageSize* (pageIndex - 1)).Take(pageSize).ToList();
            else
                return _unitOfWork.Product.GetList(orderBy: e => e.OrderByDescending(s => s.Price)); ;
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message, "some err");
            throw;
        }
    }

    public object? UpdateEntity(Product request)
    {
        try
        {
            var entity = _unitOfWork.Product.GetByID(request.Id);
            if (entity == null) return entity;

            _unitOfWork.Product.Update(request);
            _unitOfWork.Commit();

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message, "some err");
            throw;
        }
    }
}
