using Catalog.API.Models;
using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(cancellation))
        {
            return;
        }

        // Marten UPSERT will cater for existing records
        session.Store(GetPreconfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() =>
    [
        new()
        {
            Id = new Guid("d3c7566c-dc0e-4fa8-a8d3-60f234c5a2b3"),
            Name = "Pixel Pro 8",
            Category = ["Smartphone", "Google"],
            Description = "Google's latest flagship device with outstanding AI capabilities.",
            ImageFile = "product-1.png",
            Price = 999.99M
        },
        new()
        {
            Id = new Guid("8abf4193-4af6-426a-991c-ff98b6b8f99e"),
            Name = "Galaxy S24 Ultra",
            Category = ["Smartphone", "Samsung"],
            Description = "Samsung's premium phone featuring a powerful camera system and dynamic AMOLED display.",
            ImageFile = "product-2.png",
            Price = 1199.99M
        },
        new()
        {
            Id = new Guid("b76d4361-26b0-48d2-bcd5-ebed9875fa89"),
            Name = "iPhone 15 Mini",
            Category = ["Smartphone", "Apple"],
            Description = "Apple's compact powerhouse designed for one-handed use, offering excellent performance.",
            ImageFile = "product-3.png",
            Price = 799.00M
        },
        new()
        {
            Id = new Guid("ed4f9f6c-c7ba-4768-9371-6f292fdcd0b1"),
            Name = "Xperia Pro II",
            Category = ["Smartphone", "Sony"],
            Description = "Sony's phone aimed at creators, excelling in professional-grade photo and video capture.",
            ImageFile = "product-4.png",
            Price = 1299.50M
        },
        new()
        {
            Id = new Guid("c2384e06-5618-4634-b9b8-58dfc70bdb1f"),
            Name = "Moto G Power",
            Category = ["Smartphone"],
            Description = "A budget-friendly choice with a long-lasting battery and reliable performance.",
            ImageFile = "product-5.png",
            Price = 249.99M
        },
        new()
        {
            Id = new Guid("b1a77f3e-c4c9-4e75-8e22-d63451569c90"),
            Name = "iPhone 13 Pro",
            Category = ["Smartphone", "Apple"],
            Description = "Apple's flagship smartphone with a Super Retina XDR display and A15 Bionic chip.",
            ImageFile = "product-6.png",
            Price = 999.99M
        },
        new()
        {
            Id = new Guid("24f5970e-a9f5-46d1-bfa1-45c51563f6d9"),
            Name = "iPhone SE (3rd Gen)",
            Category = ["Smartphone", "Apple"],
            Description = "Compact and powerful, featuring Apple's A15 Bionic chip and 5G capabilities.",
            ImageFile = "product-7.png",
            Price = 429.99M
        },
        new()
        {
            Id = new Guid("7c5b4d1e-6ea1-403e-9b6f-e91b1589c882"),
            Name = "Galaxy S23",
            Category = ["Smartphone", "Samsung"],
            Description = "Samsung's premium smartphone with a dynamic AMOLED display and excellent camera features.",
            ImageFile = "product-8.png",
            Price = 849.99M
        }
    ];
}
