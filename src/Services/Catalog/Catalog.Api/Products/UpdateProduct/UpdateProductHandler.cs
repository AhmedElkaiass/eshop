namespace Catalog.Api.Products.UpdateProduct;
public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, List<string> Category) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id Is Required");
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3)
            .WithMessage("Name Is Required and length must be more than 3");
        RuleFor(x => x.Description).NotEmpty().MinimumLength(10)
            .WithMessage("Description Is Required and length must be more than 10");
        RuleFor(x => x.Price).GreaterThan(0)
            .WithMessage("Price Is Required and length must be more than 0");
        RuleFor(x => x.Category).NotEmpty()
            .WithMessage("Category Is Required");
    }
}
public class UpdateProductHandler(IDocumentSession _session, ILogger<UpdateProductHandler> _logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating product with id {id}", command.Id);
        var product = await _session.LoadAsync<Product>(command.Id);
        if (product is null)
            throw new ProductNotFoundException(command.Id);
        product.Name = command.Name;
        product.Description = command.Description;
        product.Price = command.Price;
        product.Category = command.Category;
        _session.Update<Product>(product);
        await _session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
    }
}
