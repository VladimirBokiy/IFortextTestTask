using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class MyOrderService : IOrderService
    {
        private ApplicationDbContext _dbContext;

        public MyOrderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> GetOrder()
        {
            return await _dbContext.Orders
                .Where(o => o.Quantity > 1)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _dbContext.Orders
                .Where(o => o.User.Status == Enums.UserStatus.Active)
                .OrderBy(o => o.CreatedAt)
                .ToListAsync();
        }
    }
}
