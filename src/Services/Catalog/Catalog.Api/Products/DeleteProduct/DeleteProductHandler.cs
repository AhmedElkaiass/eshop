
namespace Catalog.Api.Products.DeleteProduct;
public record DeleteProductResponse(bool Success);
public record class DeleteProductCommand(Guid Id) : ICommand<DeleteProductResponse>;
public class DeleteProductHandler(IDocumentSession _session,
                                  ILogger<DeleteProductHandler> _logger) : ICommandHandler<DeleteProductCommand, DeleteProductResponse>
{
    public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting product with id: {Id}", request.Id);
        var product = await _session.LoadAsync<Product>(request.Id)
            ?? throw new ProductNotFoundException(request.Id);

        _session.Delete<Product>(request.Id);
        await _session.SaveChangesAsync(cancellationToken);
        return new(true);
    }
}