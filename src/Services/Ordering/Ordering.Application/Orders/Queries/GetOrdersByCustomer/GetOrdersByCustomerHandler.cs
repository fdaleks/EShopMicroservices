﻿namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(x => x.OrderItems).AsNoTracking()
            .Where(x => x.CustomerId == CustomerId.Of(query.CustomerId))
            .OrderBy(x => x.OrderName.Value)
            .ToListAsync(cancellationToken);

        var result = orders.ToOrderDtoList();
        return new GetOrdersByCustomerResult(result);
    }
}
