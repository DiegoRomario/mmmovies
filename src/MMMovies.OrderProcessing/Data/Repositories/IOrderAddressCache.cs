using Ardalis.Result;
using MMMovies.OrderProcessing.Data.Cache;

namespace MMMovies.OrderProcessing.Data.Repositories;

internal interface IOrderAddressCache
{
    Task<Result<OrderAddress>> GetByIdAsync(Guid addressId);
    Task<Result> StoreAsync(OrderAddress orderAddress);
}
