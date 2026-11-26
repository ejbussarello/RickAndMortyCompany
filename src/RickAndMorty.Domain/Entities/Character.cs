using System.Reflection.Metadata;

namespace RickAndMorty.Domain.Entities;

public class Character
{
    public int Id { get; private set; }
    public int ExternalId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Status { get; private set; } = string.Empty;
    public string Species { get; private set; } = string.Empty;
    public DateTime ImportedAt { get; private set; }
    public string Type { get; private set; } = string.Empty;
    public string Gender { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;
    public DateTime Created { get; private set; }
    public string OriginName { get; private set; } = string.Empty;
    public string OriginUrl { get; private set; } = string.Empty;
    public string LocationName { get; private set; } = string.Empty;
    public string LocationUrl { get; private set; } = string.Empty;
    public string EpisodeUrl { get; private set; } = string.Empty;

    private Character() { }

    public Character(
        int externalId, 
        string name, 
        string status, 
        string species, 
        string type, 
        string gender, 
        string image, 
        string url, 
        DateTime created, 
        string originName, 
        string originUrl, 
        string locationName, 
        string locationUrl, 
        IEnumerable<string> episodeUrls)
    {
        ExternalId = externalId;
        Name = name;
        Status = status;
        Species = species;
        ImportedAt = DateTime.UtcNow;
        Type = type;
        Gender = gender;
        Image = image;
        Url = url;
        Created = created;
        OriginName = originName;
        OriginUrl = originUrl;
        LocationName = locationName;
        LocationUrl = locationUrl;
        EpisodeUrl = string.Join(",", episodeUrls);
    }
}