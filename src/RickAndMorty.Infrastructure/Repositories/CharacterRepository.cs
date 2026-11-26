using Microsoft.EntityFrameworkCore;
using RickAndMorty.Application.Interfaces;
using RickAndMorty.Domain.Entities;
using RickAndMorty.Infrastructure.Data;

namespace RickAndMorty.Infrastructure.Repositories;

public class CharacterRepository : ICharacterRepository
{
    private readonly AppDbContext _context;

    public CharacterRepository(AppDbContext context) => _context = context;

    public Task<bool> ExistsByExternalIdAsync(int externalId, CancellationToken ct = default)
        => _context.Characters.AnyAsync(c => c.ExternalId == externalId, ct);

    public async Task AddRangeAsync(IEnumerable<Character> characters, CancellationToken ct = default)
        => await _context.Characters.AddRangeAsync(characters, ct);

    public async Task<IReadOnlyList<Character>> GetAllAsync(CancellationToken ct = default)
        => await _context.Characters.AsNoTracking().ToListAsync(ct);

    public Task SaveChangesAsync(CancellationToken ct = default)
        => _context.SaveChangesAsync(ct);
}