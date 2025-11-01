using Microsoft.EntityFrameworkCore;
using Cefalo.Csharp.Core.Entities;
using Cefalo.Csharp.Core.Repositories;
using Cefalo.Csharp.Infrastructure.Data;

namespace Cefalo.Csharp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TaskManagementDbContext _context;

    public UserRepository(TaskManagementDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Tickets.Where(t => !t.Deleted))
            .Where(u => !u.Deleted)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Include(u => u.Tickets.Where(t => !t.Deleted))
            .Where(u => !u.Deleted)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Tickets.Where(t => !t.Deleted))
            .Where(u => !u.Deleted)
            .ToListAsync();
    }

    public async Task<User> AddAsync(User user)
    {
        user.Deleted = false;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users
            .Include(u => u.Tickets.Where(t => !t.Deleted))
            .Where(u => !u.Deleted)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user != null)
        {
            foreach (var ticket in user.Tickets)
            {
                ticket.Deleted = true;
            }

            user.Deleted = true;
            await _context.SaveChangesAsync();
        }
    }
}