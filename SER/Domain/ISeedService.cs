using AutoMapper;
using SER.Domain.Services;

namespace SER.Domain;

public interface ISeedService
{
    ICustomerService Customer { get; }
    IShopService Shop { get; }
    IProductService Product { get; }
}
public class SeedService : ISeedService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    public SeedService(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = loggerFactory.CreateLogger("logs");
        _mapper = mapper;
    }

    private ICustomerService? _customerService;
    public ICustomerService Customer => _customerService ?? new CustomerService(_unitOfWork, _logger, _mapper);

    private IShopService? _ShopService;
    public IShopService Shop => _ShopService ?? new ShopService(_unitOfWork, _logger, _mapper);

    private IProductService? _ProductService;
    public IProductService Product => _ProductService ?? new ProductService(_unitOfWork, _logger, _mapper);
}
