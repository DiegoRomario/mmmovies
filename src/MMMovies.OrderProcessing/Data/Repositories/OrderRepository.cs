using Microsoft.EntityFrameworkCore;
using MMMovies.OrderProcessing.Domain;

namespace MMMovies.OrderProcessing.Data.Repositories;

internal class OrderRepository(OrderProcessingDbContext dbContext) : IOrderRepository
{
    private readonly OrderProcessingDbContext _dbContext = dbContext;

    public async Task AddAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
    }

    public async Task<List<Order>> ListAsync()
    {
        return await _dbContext.Orders
          .Include(o => o.OrderItems)
          .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
