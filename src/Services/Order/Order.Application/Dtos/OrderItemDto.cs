namespace Order.Application.Dtos;

public record OrderItemDto(
    Guid ProductId,
    Guid OrderId,
    int Quantity,
    decimal Price
);
