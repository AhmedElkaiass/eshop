namespace Catalog.Api.Products.GetProducts;

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsQuery request, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(request, cancellationToken);
            if (result is null || !result.Products.Any())
                return Results.NotFound();
            return Results.Ok(result);
        }).WithName("GetProducts")
            .WithDescription("Get All Products")
            .Produces<GetProductsResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}