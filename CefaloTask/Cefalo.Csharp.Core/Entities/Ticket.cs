using System.Text.Json.Serialization;

namespace Cefalo.Csharp.Core.Entities;

public class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public TicketStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public bool Deleted { get; set; } = false;

    [JsonIgnore]
    public User User { get; set; } = null!;
}

public enum TicketStatus
{
    Todo,
    InProgress,
    Completed,
    Cancelled
}

public enum TicketPriority
{
    Low,
    Medium,
    High,
    Critical
}