namespace Catalog.Api.Products.CreateProduct;
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode("500.00.1")
            .WithMessage("Name is required");
        RuleFor(x => x.Category)
            .NotEmpty()
            .WithErrorCode("500.00.1")
            .WithMessage("Category is required");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithErrorCode("500.00.1")
            .WithMessage("Description is required");
        RuleFor(x => x.ImageFile)
            .NotEmpty()
            .WithErrorCode("500.00.1")
            .WithMessage("ImageFile is required");
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithErrorCode("500.00.1")
            .WithMessage("Price must be greater than 0");
    }
}
public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
internal class CreateProductCommandHandler(IDocumentSession _session, IValidator<CreateProductCommand> _validator) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new()
        {
            Name = request.Name,
            Category = request.Category,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Price = request.Price
        };
        _session.Store(product);
        await _session.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id);
    }
}