using Discount.Grpc.Data;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;
public class DiscountService(DiscountContext _dbContext,
                             ILogger<DiscountService> _logger) : Discount.DiscountBase
{
    public override async Task<CoponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var copone = await _dbContext.CoponModels.FirstOrDefaultAsync(x => x.ProductName==request.ProductName);
        if (copone is null)
            copone=new CoponModel()
            {
                Amount=0,
                Discription="No Discount Avilable.",
                ProductName="No Product",
            };
        _logger.LogInformation("discount is recevid for the product: {productName}", copone.ProductName);
        return copone.Adapt<CoponModel>();

    }
    public override async Task<CoponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        CoponModel copone = request.Copon.Adapt<CoponModel>();
        if (copone is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Copon is null"));
        _dbContext.CoponModels.Add(copone);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("discount is created for the product: {productName}", copone.ProductName);
        return copone;
    }


    public override async Task<CoponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        CoponModel copone = request.Copon.Adapt<CoponModel>();
        if (copone is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Copon is null"));
        _dbContext.CoponModels.Update(copone);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("discount is updated for the product: {productName}", copone.ProductName);
        return copone;
    }
    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var copone = await _dbContext.CoponModels.FirstOrDefaultAsync(x => x.ProductName==request.ProductName);
        if (copone is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Copon for the product is invalid"));
        _dbContext.CoponModels.Remove(copone);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("discount is deleted for the product: {productName}", copone.ProductName);
        return new DeleteDiscountResponse()
        {
            Success=true,
        };
    }
}
