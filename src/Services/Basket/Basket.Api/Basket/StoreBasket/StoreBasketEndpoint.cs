namespace Basket.Api.Basket.StoreBasket;

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", MapStoreBasketEndpoint)
            .WithName("StoreBasket")
            .WithDescription("Store Basket")
            .Produces<StoreBasketResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
    private async Task<IResult> MapStoreBasketEndpoint(StoreBasketCommand request, ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);
        return Results.Ok(result);
    }
}