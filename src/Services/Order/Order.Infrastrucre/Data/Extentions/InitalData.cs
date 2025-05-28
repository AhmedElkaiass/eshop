
namespace Order.Infrastrucre.Data.Extentions;

internal class InitalData
{
    public static Customer[] Customers =>
    [
        Customer.Create(CustomerId.Of(Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662DA")),
                        "ahmed",
                        "am310471@gmail.com",
                        "567016337"),
        Customer.Create(CustomerId.Of(Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662AA")),
                        "yamin",
                        "yamin@gmail.com",
                        "567016338"),
        Customer.Create(CustomerId.Of(Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662AB")),
                        "heba",
                        "heba@gmail.com",
                        "567016339"),
    ];
    public static Product[] Products
        => [
            Product.Creat(
                ProductId.Of(Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662DC")),
                "Product 1",
                100),
            Product.Creat(
                ProductId.Of(Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662DD")),
                "Product 2",
                200),
            Product.Creat(
                ProductId.Of(Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662DE")),
                "Product 3",
                300),
            Product.Creat(
                ProductId.Of(Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662DF")),
                "Product 4",
                400),
            ];
    public static Domain.Entities.Order[] Orders
    {
        get
        {
            Address address1 = Address.Of("Ahmed", "Elkaiass", "mail@google.com", "mansoura.1 th ff", "4555", "egypt", "mansoura");
            Address address2 = Address.Of("Mohamed", "elkaiass", "mail2@google.com", "mansoura.2 th ff", "4555", "egypt", "mansoura");
            Payment payment1 = Payment.Of("1234567895550", "12/30", "Mada", "336", 1);
            Payment payment2 = Payment.Of("1234567895551", "12/30", "Visa", "336", 2);
            var order1 = Domain.Entities.Order.Create(
                OrderId.Of(Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662E0")),
                CustomerId.Of(Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662DA")),
                OrderName.Of("Order 1"),
                address1,
                address2,
                payment1);
            order1.Add(ProductId.Of(Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662DC")), 2, 120);
            order1.Add(ProductId.Of(Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662DD")), 1, 120);
            var order2 = Domain.Entities.Order.Create(
               OrderId.Of(Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662E1")),
               CustomerId.Of(Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662AA")),
               OrderName.Of("Order 2"),
               address1,
               address2,
               payment1);
            order2.Add(ProductId.Of(Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662DF")), 2, 400);
            order2.Add(ProductId.Of(Guid.Parse("6B29FC40-CA47-1067-B31D-00DD010662DE")), 1, 300);
            return [order1, order2];
        }
    }
}
