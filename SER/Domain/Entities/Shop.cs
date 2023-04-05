using System.ComponentModel.DataAnnotations;

namespace SER.Domain.Entities;

public class Shop
{
    // chưa config fluent api
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
