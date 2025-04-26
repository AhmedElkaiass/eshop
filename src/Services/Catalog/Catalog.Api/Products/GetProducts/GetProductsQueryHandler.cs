using Marten.Pagination;

namespace Catalog.Api.Products.GetProducts;

public record GetProductsResult(IEnumerable<Product> Products, long TotalItemCount, bool hasNext, bool hasPrev);
public record GetProductsQuery(int PageNumber = 1, int PageSize = 10) : IQuery<GetProductsResult>;
public class GetProductsQueryHandler(IDocumentSession _session) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _session.Query<Product>()
                                     .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);


        return new(products, products.TotalItemCount, products.HasNextPage, products.HasPreviousPage);
    }
}