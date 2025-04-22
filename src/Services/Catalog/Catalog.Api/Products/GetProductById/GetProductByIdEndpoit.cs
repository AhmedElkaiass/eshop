namespace Catalog.Api.Products.GetProductById;

public class GetProductByIdEndpoit : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:Guid}", handler)
            .WithName("GetProductById")
            .WithDescription("Get Product by Id")
            .Produces<GetProductResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }

    async Task<IResult> handler(ISender sender, Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetProductByIdQuery(id), cancellationToken);
        if (result is null || result.Product is null)
            return Results.NotFound();
        return Results.Ok(result);
    }
}