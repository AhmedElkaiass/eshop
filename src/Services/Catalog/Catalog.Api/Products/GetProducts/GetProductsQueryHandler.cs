using Catalog.Api.Models;

namespace Catalog.Api.Products.GetProducts;

public record GetProductsResult(IEnumerable<Product> Products);
public record GetProductsQuery() : IQuery<GetProductsResult>;
public class GetProductsQueryHandler(IDocumentSession _session,
                                     ILogger<GetProductsQueryHandler> _logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all products");
        var products = await _session.Query<Product>()
                                     .ToListAsync(cancellationToken);
        return new(products);
    }
}