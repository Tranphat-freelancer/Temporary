using AutoMapper;
using SER.Domain.Entities;
using SER.ViewModel.Shop.Requests;

namespace SER.Domain.Services;

public interface IShopService
{
    object? GetAll();
    object? GetPaged(int pageIndex, int pageSize);
    object? GetEntity(object id);
    object? CreateEntity(ShopCreateRequest request);
    object? UpdateEntity(ShopUpdateRequest request);
    object? DeleteEntity(object id);
}
public class ShopService : IShopService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public ShopService(IUnitOfWork unitOfWork, ILogger logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public object? CreateEntity(ShopCreateRequest request)
    {
        try
        {
            var entity = _mapper.Map<Shop>(request);

            _unitOfWork.Shop.Insert(entity);
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
            var qShop = _unitOfWork.Shop.GetQuery(orderBy: e => e.OrderByDescending(s => s.Location));
            var totalRow = qShop.Count();
            if (pageIndex > 0 && pageSize > 0)
                return new
                {
                    Data = qShop.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList(),
                    totalRow
                };
            else
                return new
                {
                    Data = qShop.ToList(),
                    totalRow
                };
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message, "some err");
            throw;
        }
    }

    public object? UpdateEntity(ShopUpdateRequest request)
    {
        try
        {
            var entity = _unitOfWork.Shop.GetByID(request.Id);
            if (entity == null) return entity;

            _mapper.Map(request, entity);

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
