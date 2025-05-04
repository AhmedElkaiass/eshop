namespace Basket.Api.Basket.DeleteBasket
{
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{useName}", async (string useName, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(useName), cancellationToken);
                return Results.Ok(result);
            })
            .WithName("DeleteBasket")
            .WithDescription("Delete Basket")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);
        }

    }
}
