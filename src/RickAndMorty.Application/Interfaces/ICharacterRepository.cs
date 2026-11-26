using RickAndMorty.Domain.Entities;

namespace RickAndMorty.Application.Interfaces;

public interface ICharacterRepository
{
    Task<bool> ExistsByExternalIdAsync(int externalId, CancellationToken ct = default);
    Task AddRangeAsync(IEnumerable<Character> characters, CancellationToken ct = default);
    Task<IReadOnlyList<Character>> GetAllAsync(CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}