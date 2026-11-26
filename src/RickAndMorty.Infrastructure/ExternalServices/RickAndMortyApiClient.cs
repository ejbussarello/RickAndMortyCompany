using System.Net.Http.Json;
using RickAndMorty.Application.Interfaces;

namespace RickAndMorty.Infrastructure.ExternalServices;

public class RickAndMortyApiClient : IRickAndMortyApiClient
{
    private readonly HttpClient _httpClient;

    public RickAndMortyApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://rickandmortyapi.com/api/");
    }

    public async Task<IReadOnlyList<ExternalCharacterDto>> GetCharactersAsync(
        int page = 1, CancellationToken ct = default)
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse>(
            $"character?page={page}", ct);

        return response?.Results?
            .Select(r => new ExternalCharacterDto(
                r.Id, 
                r.Name, 
                r.Status, 
                r.Species,
                r.Type,
                r.Gender,
                r.Image,
                r.Url,
                r.Created,
                r.Origin?.Name ?? string.Empty,
                r.Origin?.Url ?? string.Empty,
                r.Location?.Name ?? string.Empty,
                r.Location?.Url ?? string.Empty,
                r.Episode ?? new List<string>()))
            .ToList() ?? new List<ExternalCharacterDto>();
    }

    private class ApiResponse
    {
        public List<ApiCharacter> Results { get; set; } = new();
    }

    private class ApiCharacter
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public ApiOrigin? Origin { get; set; }
        public ApiLocation? Location { get; set; }
        public List<string>? Episode { get; set; }

    }

    private class ApiOrigin
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }

    private class ApiLocation
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}