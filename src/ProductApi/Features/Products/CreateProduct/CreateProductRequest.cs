using System.ComponentModel.DataAnnotations;

namespace ProductApi.Features.Products.CreateProduct;

public sealed record CreateProductRequest
{
    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Product name must be between 2 and 100 characters.")]
    public required string Name { get; init; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string? Description { get; init; }

    [Range(typeof(decimal), "0.01", "999999999.99", ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative.")]
    public int StockQuantity { get; init; }
}
