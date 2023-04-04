using System.ComponentModel.DataAnnotations;

namespace SER.Models;

public class Customer
{
    // chưa config fluent api
    [Key]
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime? DOB { get; set; }
    public string Email { get; set; } = string.Empty;
}
