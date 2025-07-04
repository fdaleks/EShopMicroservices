namespace Shopping.Web.Models.Catalog;

public class ProductModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public List<string> Category { get; set; } = [];
    public required string Description { get; set; }
    public required string ImageFile { get; set; }
    public decimal Price { get; set; }
}

// wrapper classes
public record GetProductsResponse(IEnumerable<ProductModel> Products);
public record GetProductByIdResponse(ProductModel Product);
public record GetProductByCategoryResponse(IEnumerable<ProductModel> Products);
