using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using Cefalo.Csharp.Core.DTOs;

public class UsersModel : PageModel
{
    public List<UserDto>? Users { get; set; }
    public string? ErrorMessage { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5039/");

            var response = await httpClient.GetAsync("api/users");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(jsonString);

                if (jsonDoc.RootElement.TryGetProperty("$values", out var valuesElement))
                {
                    Users = JsonSerializer.Deserialize<List<UserDto>>(valuesElement.GetRawText(), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            else
            {
                ErrorMessage = $"API returned status code: {response.StatusCode}";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error: {ex.Message}";
        }
    }
}

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}