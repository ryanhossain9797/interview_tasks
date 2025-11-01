using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using Cefalo.Csharp.Core.Entities;
using Cefalo.Csharp.Core.DTOs;

public class TicketsModel : PageModel
{
    public List<TicketDto>? Tickets { get; set; }

    public async Task OnGetAsync()
    {
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5039/");

        var response = await httpClient.GetAsync("api/tickets");

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var jsonDoc = JsonDocument.Parse(jsonString);

            if (jsonDoc.RootElement.TryGetProperty("$values", out var valuesElement))
            {
                Tickets = JsonSerializer.Deserialize<List<TicketDto>>(valuesElement.GetRawText(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
    }
}

public class TicketDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public TicketStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
}