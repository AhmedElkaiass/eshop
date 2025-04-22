

namespace Catalog.Api.Products.GetProductById;

public record GetProductResult(Product Product);
public record GetProductByIdQuery(Guid Id) : IQuery<GetProductResult>;
public class GetProductByIdHandler(IDocumentSession _session, ILogger<GetProductByIdHandler> _logger) : IQueryHandler<GetProductByIdQuery, GetProductResult>
{

    public async Task<GetProductResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting product with id {id}", request.Id);
        var product = await _session.LoadAsync<Product>(request.Id, cancellationToken);
        if (product is null)
        {
            _logger.LogWarning("Product with id {id} not found", request.Id);
            throw new ProductNotFoundException(request.Id);
        }
        return new(product);
    }
}