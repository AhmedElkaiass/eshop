using Carter;

namespace Basket.Api.Basket.GetBasket
{
    public class GetBasketEndpoint : ICarterModule

    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{UserName:string}", MapGetBasketEndpoint)
                .WithName("GetBasketByuser")
                .WithDescription("Get Basket By UserName")
                .Produces<GetBasketResult>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                ;
        }

        private async Task<IResult> MapGetBasketEndpoint(string UserName, ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetBasketQuery(UserName), cancellationToken);
            return Results.Ok(result);
        } 
    }
}
