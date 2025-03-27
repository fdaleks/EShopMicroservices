namespace Discount.Grpc.Services;

public class DiscountService(DataContext dataContext, ILogger<DiscountService> logger) 
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>() 
            ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        dataContext.Coupons.Add(coupon);
        await dataContext.SaveChangesAsync();

        logger.LogInformation("Discount created for ProductName: {productName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    // TODO: rewrite for using id instead of productName
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dataContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        coupon ??= new Coupon { Id = 0, ProductName = "No Discount", Description = "No Discount", Amount = 0 };

        logger.LogInformation("Discount retrieved for ProductName: {productName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dataContext.Coupons.FindAsync(request.Coupon.Id) 
            ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        request.Coupon.Adapt(coupon);
        await dataContext.SaveChangesAsync();

        logger.LogInformation("Discount updated for ProductName: {productName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    // TODO: rewrite for using id instead of productName
    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dataContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName)
            ?? throw new RpcException(new Status(StatusCode.NotFound, $"Discount with {request.ProductName} productName not found"));

        dataContext.Coupons.Remove(coupon);
        await dataContext.SaveChangesAsync();

        logger.LogInformation("Discount removed for ProductName: {productName}", coupon.ProductName);

        var response = new DeleteDiscountResponse { Success = true };
        return response;
    }
}
