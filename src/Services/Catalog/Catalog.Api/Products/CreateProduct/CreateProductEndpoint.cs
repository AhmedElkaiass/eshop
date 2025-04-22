
namespace Catalog.Api.Products.CreateProduct;
public record CreateProductRequest(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
);
public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", handler)
            .WithName("CreateProduct")
            .Produces<CreateProductResult>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create a new Product endpoint")
            ;
    }
    async Task<IResult> handler(ISender sender, CreateProductRequest request, CancellationToken token = default)
    {
        var command = request.Adapt<CreateProductCommand>();
        CreateProductResult result = await sender.Send(command);
        return Results.Created($"/products/{result.Id}", result);
    }
}
