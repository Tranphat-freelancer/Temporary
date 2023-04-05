namespace SER.ViewModel.Product.Requests;

public class ProductUpdateRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
