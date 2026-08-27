using System.Collections.Concurrent;

namespace ProductApi.Features.Products.CreateProduct;

public sealed class CreateProductService
{
    private readonly ConcurrentDictionary<Guid, Product> _products = new();
    private readonly ConcurrentDictionary<string, byte> _productNames =
        new(StringComparer.OrdinalIgnoreCase);

    public CreateProductResult Create(CreateProductRequest request)
    {
        var normalizedName = request.Name.Trim();

        if (!_productNames.TryAdd(normalizedName, 0))
        {
            return CreateProductResult.Conflict(
                $"A product named '{normalizedName}' already exists.");
        }

        var product = new Product(
            Guid.NewGuid(),
            normalizedName,
            string.IsNullOrWhiteSpace(request.Description)
                ? null
                : request.Description.Trim(),
            request.Price,
            request.StockQuantity,
            DateTime.UtcNow);

        if (!_products.TryAdd(product.Id, product))
        {
            _productNames.TryRemove(normalizedName, out _);
            throw new InvalidOperationException("The product could not be stored.");
        }

        return CreateProductResult.Success(new CreateProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.StockQuantity,
            product.CreatedAtUtc));
    }

    private sealed record Product(
        Guid Id,
        string Name,
        string? Description,
        decimal Price,
        int StockQuantity,
        DateTime CreatedAtUtc);
}

public sealed record CreateProductResult(
    CreateProductResponse? Product,
    string? Error)
{
    public bool IsSuccess => Product is not null;

    public static CreateProductResult Success(CreateProductResponse product) =>
        new(product, null);

    public static CreateProductResult Conflict(string error) =>
        new(null, error);
}
