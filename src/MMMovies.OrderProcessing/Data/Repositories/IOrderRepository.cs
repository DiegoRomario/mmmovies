using MMMovies.OrderProcessing.Domain;

namespace MMMovies.OrderProcessing.Data.Repositories;

internal interface IOrderRepository
{
    Task<List<Order>> ListAsync();
    Task AddAsync(Order order);
    Task SaveChangesAsync();
}
