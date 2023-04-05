using AutoMapper;
using SER.Domain.Entities;
using SER.ViewModel.Customer.Requests;

namespace SER.Domain.Services;

public interface ICustomerService
{
    object? GetAll();
    object? GetPaged(int pageIndex, int pageSize);
    object? GetEntity(object id);
    object? CreateEntity(CustomerCreateRequest request);
    object? UpdateEntity(CustomerUpdateRequest request);
    object? DeleteEntity(object id);
}
public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    public CustomerService(IUnitOfWork unitOfWork, ILogger logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;

    }

    public object? CreateEntity(CustomerCreateRequest request)
    {
        try
        {
            var entity = _mapper.Map<Customer>(request);

            _unitOfWork.Customer.Insert(entity);
            _unitOfWork.Commit();

            return entity;
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
            var qCustomer = _unitOfWork.Customer.GetQuery(orderBy: e => e.OrderBy(s => s.Email));
            var totalRow = qCustomer.Count();
            if (pageIndex > 0 && pageSize > 0)
                return new
                {
                    totalRow,
                    Data = qCustomer.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList(),
                };
            else
                return new
                {
                    totalRow,
                    Data = qCustomer.ToList(),
                };
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message, "some err");
            throw;
        }
    }

    public object? UpdateEntity(CustomerUpdateRequest request)
    {
        try
        {
            var entity = _unitOfWork.Customer.GetByID(request.Id);
            if (entity == null) return entity;

            _mapper.Map(request, entity);

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
