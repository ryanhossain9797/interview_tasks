using System.Text.Json.Serialization;

namespace Cefalo.Csharp.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Deleted { get; set; } = false;

    [JsonIgnore]
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}