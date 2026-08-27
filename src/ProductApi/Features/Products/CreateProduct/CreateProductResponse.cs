namespace ProductApi.Features.Products.CreateProduct;

public sealed record CreateProductResponse(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int StockQuantity,
    DateTime CreatedAtUtc);
