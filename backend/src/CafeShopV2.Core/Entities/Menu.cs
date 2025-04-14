namespace CafeShopV2.Core.Entities;

public sealed class Menu
{
    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    public string? Description { get; set; }
    
    public required string Category { get; set; }
    
    public decimal Price { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public bool IsAvailable { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
} 