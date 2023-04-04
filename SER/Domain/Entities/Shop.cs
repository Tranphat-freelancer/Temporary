using System.ComponentModel.DataAnnotations;

namespace SER.Domain.Entities;

public class Shop
{
    // chưa config fluent api
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}
