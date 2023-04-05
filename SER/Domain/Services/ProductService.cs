using AutoMapper;
using SER.Domain.Entities;
using SER.ViewModel.Product.Requests;

namespace SER.Domain.Services;

public interface IProductService
{
    object? GetAll();
    object? GetPaged(int pageIndex, int pageSize);
    object? GetEntity(object id);
    object? CreateEntity(ProductCreateRequest request);
    object? UpdateEntity(ProductUpdateRequest request);
    object? DeleteEntity(object id);
}
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    public ProductService(IUnitOfWork unitOfWork, ILogger logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public object? CreateEntity(ProductCreateRequest request)
    {
        try
        {
            var entity = _mapper.Map<Product>(request);

            _unitOfWork.Product.Insert(entity);
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
            var qProduct = _unitOfWork.Product.GetQuery(orderBy: e => e.OrderByDescending(s => s.Price));
            var totalRow = qProduct.Count();
            if (pageIndex > 0 && pageSize > 0)
                return new
                {
                    totalRow,
                    Data = qProduct.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList(),
                };
            else
                return new
                {
                    totalRow,
                    Data = qProduct.ToList(),
                };
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message, "some err");
            throw;
        }
    }

    public object? UpdateEntity(ProductUpdateRequest request)
    {
        try
        {
            var entity = _unitOfWork.Product.GetByID(request.Id);
            if (entity == null) return entity;

            _mapper.Map(request, entity);

            _unitOfWork.Product.Update(entity);
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
