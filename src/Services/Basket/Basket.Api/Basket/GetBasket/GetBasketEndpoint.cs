namespace Basket.Api.Basket.GetBasket
{
    public class GetBasketEndpoint : ICarterModule

    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", MapGetBasketEndpoint)
                .WithName("GetBasketByuser")
                .WithDescription("Get Basket By UserName")
                .Produces<GetBasketResult>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
            ;
        }

        private async Task<IResult> MapGetBasketEndpoint(string userName, ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetBasketQuery(userName), cancellationToken);
            return Results.Ok(result);
        }
    }
}
