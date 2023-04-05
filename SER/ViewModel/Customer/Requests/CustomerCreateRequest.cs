namespace SER.ViewModel.Customer.Requests;

public class CustomerCreateRequest
{
    public string FullName { get; set; } = string.Empty;
    public DateTime? DOB { get; set; }
    public string Email { get; set; } = string.Empty;
}
