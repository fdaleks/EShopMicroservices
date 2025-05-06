namespace Ordering.Infrastructure.Data;

internal class OrderingInitialData
{
    public static IEnumerable<Customer> Customers =>
    [
        Customer.Create(CustomerId.Of(new Guid("9f507f8f-305d-4032-81a6-32cabc6688a4")), "john", "johnsmith@mail.com"),
        Customer.Create(CustomerId.Of(new Guid("7b2b8f3f-8bfb-4d86-bdac-129d7f7da823")), "sam", "sambrown@mail.com")
    ];

    public static IEnumerable<Product> Products =>
    [
        Product.Create(ProductId.Of(new Guid("d3c7566c-dc0e-4fa8-a8d3-60f234c5a2b3")), "Pixel Pro 8", 999.99M),
        Product.Create(ProductId.Of(new Guid("8abf4193-4af6-426a-991c-ff98b6b8f99e")), "Galaxy S24 Ultra", 1199.99M),
        Product.Create(ProductId.Of(new Guid("b76d4361-26b0-48d2-bcd5-ebed9875fa89")), "iPhone 15 Mini", 799.00M),
        Product.Create(ProductId.Of(new Guid("ed4f9f6c-c7ba-4768-9371-6f292fdcd0b1")), "Xperia Pro II", 1299.50M)
    ];
    
    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of("john", "smith", "johnsmith@mail.com", "3077 Hartland Avenue", "Green Bay", "Wisconsin", "54302", "US");
            var address2 = Address.Of("sam", "brown", "sambrown@mail.com", "2832 Modoc Alley", "Pocatello", "Idaho", "83202", "US");

            var payment1 = Payment.Of("john smith", "375708249174231", "08/25", "947", 1);
            var payment2 = Payment.Of("sam brown", "372186423145181", "12/26", "367", 1);

            var order1 = Order.Create(
                OrderId.Of(new Guid("2ac7eab4-d9bf-4683-adc7-d91f76615168")),
                CustomerId.Of(new Guid("9f507f8f-305d-4032-81a6-32cabc6688a4")),
                OrderName.Of("3KVYB"),
                shippingAddress: address1,
                billingAddress: address1,
                payment1);
            order1.AddOrderItem(ProductId.Of(new Guid("d3c7566c-dc0e-4fa8-a8d3-60f234c5a2b3")), 1, 999.99M);
            order1.AddOrderItem(ProductId.Of(new Guid("8abf4193-4af6-426a-991c-ff98b6b8f99e")), 1, 1199.99M);

            var order2 = Order.Create(
                OrderId.Of(new Guid("411b9731-769e-4d2d-a615-3d8d5c7ac070")),
                CustomerId.Of(new Guid("7b2b8f3f-8bfb-4d86-bdac-129d7f7da823")),
                OrderName.Of("VUG5U"),
                shippingAddress: address2,
                billingAddress: address2,
                payment2);
            order2.AddOrderItem(ProductId.Of(new Guid("b76d4361-26b0-48d2-bcd5-ebed9875fa89")), 1, 799.00M);
            order2.AddOrderItem(ProductId.Of(new Guid("ed4f9f6c-c7ba-4768-9371-6f292fdcd0b1")), 1, 1299.50M);

            return [order1, order2];
        }
    }
    
}
