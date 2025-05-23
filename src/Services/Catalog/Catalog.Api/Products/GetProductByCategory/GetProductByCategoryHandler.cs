﻿
namespace Catalog.Api.Products.GetProductByCategory;

public record class GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record class GetProductByCategoryResult(IEnumerable<Product> Products);
public class GetProductByCategoryHandler(IDocumentSession _session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
    {
        var products = await _session.Query<Product>()
            .Where(x => x.Category.Contains(request.Category))
            .ToListAsync(cancellationToken);
        return new GetProductByCategoryResult(products);
    }
}
