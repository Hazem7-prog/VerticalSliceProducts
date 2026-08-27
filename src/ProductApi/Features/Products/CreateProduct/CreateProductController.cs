using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Features.Products.CreateProduct;

[ApiController]
[Route("api/products")]
public sealed class CreateProductController(CreateProductService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<CreateProductResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public ActionResult<CreateProductResponse> Create(
        [FromBody] CreateProductRequest request)
    {
        var result = service.Create(request);

        if (!result.IsSuccess)
        {
            return Conflict(new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Product already exists",
                Detail = result.Error
            });
        }

        var product = result.Product!;

        return Created($"/api/products/{product.Id}", product);
    }
}
