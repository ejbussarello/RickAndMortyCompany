namespace RickAndMorty.Application.Interfaces;

public interface IRickAndMortyApiClient
{
    Task<IReadOnlyList<ExternalCharacterDto>> GetCharactersAsync(
        int page = 1, CancellationToken ct = default);
}

public record ExternalCharacterDto(
    int Id, 
    string Name, 
    string Status, 
    string Species,
    string Type,
    string Gender,
    string Image,
    string Url,
    DateTime Created,
    string OriginName,
    string OriginUrl,
    string LocationName,
    string LocationUrl,
    IReadOnlyList<string> EpisodeUrls);