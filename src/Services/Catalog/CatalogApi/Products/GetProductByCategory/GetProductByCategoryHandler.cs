namespace CatalogApi.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(x => x.Category.Any(x => x.Equals(query.Category, StringComparison.OrdinalIgnoreCase)))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}
