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
    public SeedService(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
    {
        _unitOfWork = unitOfWork;
        _logger = loggerFactory.CreateLogger("logs");
    }

    private ICustomerService? _customerService;
    public ICustomerService Customer => _customerService ?? new CustomerService(_unitOfWork, _logger);

    private IShopService? _ShopService;
    public IShopService Shop => _ShopService ?? new ShopService(_unitOfWork, _logger);

    private IProductService? _ProductService;
    public IProductService Product => _ProductService ?? new ProductService(_unitOfWork, _logger);
}
