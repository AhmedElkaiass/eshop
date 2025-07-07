using Order.Domain.Enums;

namespace Order.Application.Dtos;

public record OrderDto(
Guid Id,
Guid CustomerId,
string OrderName,
List<OrderItemDto> OrderItems,
AddressDto ShippingAddress,
AddressDto BillingAddress,
PaymentDto Payment,
OrderStatus Status
);
