using Microsoft.EntityFrameworkCore;
using Cefalo.Csharp.Core.Entities;
using Cefalo.Csharp.Core.Repositories;
using Cefalo.Csharp.Infrastructure.Data;

namespace Cefalo.Csharp.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly TaskManagementDbContext _context;

    public TicketRepository(TaskManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Ticket?> GetByIdAsync(int id)
    {
        return await _context.Tickets
            .Include(t => t.User)
            .Where(t => !t.Deleted)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        return await _context.Tickets
            .Include(t => t.User)
            .Where(t => !t.Deleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetByUserIdAsync(int userId)
    {
        return await _context.Tickets
            .Include(t => t.User)
            .Where(t => t.UserId == userId && !t.Deleted)
            .ToListAsync();
    }

    public async Task<Ticket> AddAsync(Ticket ticket)
    {
        ticket.Deleted = false;
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<Ticket> UpdateAsync(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task DeleteAsync(int id)
    {
        var ticket = await _context.Tickets
            .Where(t => !t.Deleted)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (ticket != null)
        {
            ticket.Deleted = true;
            await _context.SaveChangesAsync();
        }
    }
}