using System.ComponentModel.DataAnnotations;

namespace SER.Models;

public class Shop
{
    // chưa config fluent api
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}
