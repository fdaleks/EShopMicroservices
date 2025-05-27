using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageNumber = query.PaginationRequest.PageNumber;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await dbContext.Orders
            .Include(x => x.OrderItems)
            .OrderBy(x => x.OrderName.Value)
            .Skip(pageSize * pageNumber)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var result = new PaginatedResult<OrderDto>(pageNumber, pageSize, totalCount, orders.ToOrderDtoList());
        return new GetOrdersResult(result);
    }
}
