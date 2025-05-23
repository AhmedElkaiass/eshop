﻿namespace Order.Domain.Entities;

public class OrderItem : Entity<OrderItemId>
{
    public ProductId ProductId { get; private set; }
    public OrderId OrderId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    internal OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price)
    {
        Id = OrderItemId.Of(Guid.NewGuid());
        ProductId = productId;
        Quantity = quantity;
        Price = price;
        OrderId = orderId;
    }
}
