using RickAndMorty.Application.Interfaces;
using RickAndMorty.Domain.Entities;

namespace RickAndMorty.Application.Services;

public class CharacterImportService
{
    private readonly IRickAndMortyApiClient _apiClient;
    private readonly ICharacterRepository _repository;

    public CharacterImportService(
        IRickAndMortyApiClient apiClient,
        ICharacterRepository repository)
    {
        _apiClient = apiClient;
        _repository = repository;
    }

    public async Task<int> ImportCharactersAsync(int page = 1, CancellationToken ct = default)
    {
        var externalCharacters = await _apiClient.GetCharactersAsync(page, ct);
        var newCharacters = new List<Character>();

        foreach (var dto in externalCharacters)
        {
            if (await _repository.ExistsByExternalIdAsync(dto.Id, ct))
                continue;

            newCharacters.Add(new Character(
                dto.Id,
                dto.Name,
                dto.Status,
                dto.Species,
                dto.Type,
                dto.Gender,
                dto.Image,
                dto.Url,
                dto.Created,
                dto.OriginName,
                dto.OriginUrl,
                dto.LocationName,
                dto.LocationUrl,
                dto.EpisodeUrls));
        }

        if (newCharacters.Count == 0)
            return 0;

        await _repository.AddRangeAsync(newCharacters, ct);
        await _repository.SaveChangesAsync(ct);
        return newCharacters.Count;
    }

    public Task<IReadOnlyList<Character>> GetImportedCharactersAsync(CancellationToken ct = default)
        => _repository.GetAllAsync(ct);
}