using SER.Domain.Entities;

namespace SER.Domain.Services;

public interface IShopService
{
    object? GetAll();
    object? GetPaged(int pageIndex, int pageSize);
    object? GetEntity(object id);
    object? CreateEntity(Shop request);
    object? UpdateEntity(Shop request);
    object? DeleteEntity(object id);
}
public class ShopService : IShopService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    public ShopService(IUnitOfWork unitOfWork, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public object? CreateEntity(Shop request)
    {
        try
        {
            //mapper nếu dùng auto mapper
            //...
            _unitOfWork.Shop.Insert(request);
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
            var entity = _unitOfWork.Shop.GetByID(id);
            if (entity == null) return entity;

            _unitOfWork.Shop.Delete(entity);
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

            return _unitOfWork.Shop.GetList(orderBy: e => e.OrderByDescending(s => s.Location)); ;
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
            return _unitOfWork.Shop.GetByID(id);
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
                return _unitOfWork.Shop.GetQuery(orderBy: e => e.OrderByDescending(s => s.Location)).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            else
                return _unitOfWork.Shop.GetList(orderBy: e => e.OrderByDescending(s => s.Location)); ;
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message, "some err");
            throw;
        }
    }

    public object? UpdateEntity(Shop request)
    {
        try
        {
            var entity = _unitOfWork.Shop.GetByID(request.Id);
            if (entity == null) return entity;
            entity.Name = request.Name;
            entity.Location = request.Location;
            _unitOfWork.Shop.Update(entity);
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
