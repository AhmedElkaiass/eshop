﻿namespace Order.Domain.Entities;
public class Order : AggregateRoot<OrderId>
{

    #region Fileds:
    private readonly List<OrderItem> _orderItems = new();
    #endregion

    #region Properties:
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public DateTime OrderDate { get; private set; }
    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalPrice
    {
        get
        {
            return _orderItems.Sum(item => item.Price * item.Quantity);
        }
        private set { }
    }
    #endregion

    #region Functions : 

    public static Order Create(OrderId id,
                               CustomerId customerId,
                               OrderName orderName,
                               Address shippingAddress,
                               Address billingAddress,
                               Payment payment)
    {
        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            OrderDate = DateTime.Now,
            Status = OrderStatus.Pending
        };
        order.AddDomainEvent(new OrderCreatedEvent(order));
        return order;
    }
    public void Update(OrderName orderName,
                       Address shippingAddress,
                       Address billingAddress,
                       Payment payment,
                       OrderStatus status)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = status;
        AddDomainEvent(new OrderUpdatedEvent(this));
    }
    public void Add(ProductId productId,
                    int quantity,
                    decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        var orderItem = new OrderItem(Id, productId, quantity, price);
        _orderItems.Add(orderItem);
    }
    public void Remove(OrderItemId orderItemId)
    {
        var orderItem = _orderItems.FirstOrDefault(x => x.Id == orderItemId);
        if (orderItem is not null)
        {
            _orderItems.Remove(orderItem);
        }
    }
    #endregion
}