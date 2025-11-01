using Microsoft.AspNetCore.Mvc;
using Cefalo.Csharp.Core.Entities;
using Cefalo.Csharp.Core.Services;
using Cefalo.Csharp.Core.DTOs;

namespace Cefalo.Csharp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetTickets()
    {
        var tickets = await _ticketService.GetAllTicketsAsync();
        var ticketDtos = tickets.Select(t => new TicketDto
        {
            Id = t.Id,
            Title = t.Title,
            Status = t.Status,
            CreatedAt = t.CreatedAt,
            UserId = t.UserId
        });
        return Ok(ticketDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDto>> GetTicket(int id)
    {
        var ticket = await _ticketService.GetTicketByIdAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }

        var ticketDto = new TicketDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Status = ticket.Status,
            CreatedAt = ticket.CreatedAt,
            UserId = ticket.UserId
        };
        return Ok(ticketDto);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetTicketsByUser(int userId)
    {
        try
        {
            var tickets = await _ticketService.GetTicketsByUserAsync(userId);
            var ticketDtos = tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                Title = t.Title,
                Status = t.Status,
                CreatedAt = t.CreatedAt,
                UserId = t.UserId
            });
            return Ok(ticketDtos);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<TicketDto>> CreateTicket(TicketDto ticketDto)
    {
        try
        {
            var ticket = new Ticket
            {
                Title = ticketDto.Title,
                Status = ticketDto.Status,
                UserId = ticketDto.UserId
            };

            var createdTicket = await _ticketService.CreateTicketAsync(ticket);

            var createdTicketDto = new TicketDto
            {
                Id = createdTicket.Id,
                Title = createdTicket.Title,
                Status = createdTicket.Status,
                CreatedAt = createdTicket.CreatedAt,
                UserId = createdTicket.UserId
            };

            return CreatedAtAction(nameof(GetTicket), new { id = createdTicketDto.Id }, createdTicketDto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTicket(int id, TicketDto ticketDto)
    {
        if (id != ticketDto.Id)
        {
            return BadRequest();
        }

        try
        {
            var ticket = new Ticket
            {
                Id = ticketDto.Id,
                Title = ticketDto.Title,
                Status = ticketDto.Status,
                CreatedAt = ticketDto.CreatedAt,
                UserId = ticketDto.UserId
            };

            var updatedTicket = await _ticketService.UpdateTicketAsync(ticket);

            var updatedTicketDto = new TicketDto
            {
                Id = updatedTicket.Id,
                Title = updatedTicket.Title,
                Status = updatedTicket.Status,
                CreatedAt = updatedTicket.CreatedAt,
                UserId = updatedTicket.UserId
            };

            return Ok(updatedTicketDto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        try
        {
            await _ticketService.DeleteTicketAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}