using TestTask.Services.Interfaces;
using TestTask.Models;
using TestTask.Data;
using Microsoft.EntityFrameworkCore;
using TestTask.Enums;

namespace TestTask.Services.Implementations
{
    public class MyUserService : IUserService
    {
        private ApplicationDbContext _dbContext;
        
        public MyUserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUser()
        {
            var res = await _dbContext.Orders
                .Where(o => o.CreatedAt.Year == 2003 && o.Status == OrderStatus.Delivered)
                .GroupBy(o => o.User)
                .Select(group => new
                {
                    User = group.Key,
                    Sum = group.Sum(o => o.Quantity)
                })
                .OrderByDescending(group => group.Sum)
                .FirstOrDefaultAsync();

            return res?.User;
                
        }


        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users
                .Where(u => u.Orders.Any(o => o.CreatedAt.Year == 2010 && o.Status == OrderStatus.Paid))
                .ToListAsync();
        }
    }
}
