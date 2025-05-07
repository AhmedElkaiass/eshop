﻿namespace Basket.Api.Basket.GetBasket;
public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);
public class GetBasketHandler(IBasketRepository _basketRepository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.GetBasket(request.UserName, cancellationToken);
        return new GetBasketResult(basket!);
    }
}
