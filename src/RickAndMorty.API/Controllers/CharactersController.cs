using Microsoft.AspNetCore.Mvc;
using RickAndMorty.Application.Services;

namespace RickAndMorty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly CharacterImportService _importService;

    public CharactersController(CharacterImportService importService)
        => _importService = importService;

    [HttpPost("import")]
    public async Task<IActionResult> Import(
        [FromQuery] int page = 1, CancellationToken ct = default)
    {
        var count = await _importService.ImportCharactersAsync(page, ct);
        return Ok(new { Imported = count });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct = default)
    {
        var characters = await _importService.GetImportedCharactersAsync(ct);
        return Ok(characters);
    }
}