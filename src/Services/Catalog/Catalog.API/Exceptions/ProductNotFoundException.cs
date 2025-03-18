namespace CatalogApi.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid Id) : base("Product", Id) 
    { 
    }
}
