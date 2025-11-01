using Cefalo.Csharp.Core.Entities;

namespace Cefalo.Csharp.Core.Services;

public interface ITicketService
{
    Task<Ticket?> GetTicketByIdAsync(int id);
    Task<IEnumerable<Ticket>> GetAllTicketsAsync();
    Task<IEnumerable<Ticket>> GetTicketsByUserAsync(int userId);
    Task<Ticket> CreateTicketAsync(Ticket ticket);
    Task<Ticket> UpdateTicketAsync(Ticket ticket);
    Task DeleteTicketAsync(int id);
}