namespace Catalog.Api.Products.GetProductByCategory;

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/category/{Category}", async (string Category, ISender sender, CancellationToken cancellationToken) =>
        {
            GetProductByCategoryQuery query = new(Category);
            var result = await sender.Send(query, cancellationToken);
            return Results.Ok(result);
        })
            .WithName("GetProductsByCategory")
            .WithDescription("Get Products By Category")
            .Produces<GetProductByCategoryResult>(StatusCodes.Status200OK)
            .ProducesProblem(statusCode: StatusCodes.Status400BadRequest)
            ;
    }
}
