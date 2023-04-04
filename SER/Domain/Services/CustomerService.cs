using SER.Domain.Entities;

namespace SER.Domain.Services;

public interface ICustomerService
{
    object? GetAll();
    object? GetPaged(int pageIndex, int pageSize);
    object? GetEntity(object id);
    object? CreateEntity(Customer request);
    object? UpdateEntity(Customer request);
    object? DeleteEntity(object id);
}
public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    public CustomerService(IUnitOfWork unitOfWork, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public object? CreateEntity(Customer request)
    {
        try
        {
            //mapper nếu dùng auto mapper
            //...
            _unitOfWork.Customer.Insert(request);
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
            var entity = _unitOfWork.Customer.GetByID(id);
            if (entity == null) return entity;

            _unitOfWork.Customer.Delete(entity);
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

            return _unitOfWork.Customer.GetList(orderBy: e => e.OrderBy(s => s.Email)); ;
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
            return _unitOfWork.Customer.GetByID(id); 
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
                return _unitOfWork.Customer.GetQuery(orderBy: e => e.OrderBy(s => s.Email)).Skip(pageSize* (pageIndex - 1)).Take(pageSize).ToList();
            else
                return _unitOfWork.Customer.GetList(orderBy: e => e.OrderBy(s => s.Email)); ;
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message, "some err");
            throw;
        }
    }

    public object? UpdateEntity(Customer request)
    {
        try
        {
            var entity = _unitOfWork.Customer.GetByID(request.Id);
            if (entity == null) return entity;
            entity.DOB = request.DOB;
            entity.FullName = request.FullName;
            entity.Email = request.Email;
            _unitOfWork.Customer.Update(entity);
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
