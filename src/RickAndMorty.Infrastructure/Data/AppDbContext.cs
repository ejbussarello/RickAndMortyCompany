using Microsoft.EntityFrameworkCore;
using RickAndMorty.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RickAndMorty.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Character> Characters => Set<Character>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ExternalId).IsRequired();
            entity.HasIndex(e => e.ExternalId).IsUnique();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Species).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Image).HasMaxLength(500);
            entity.Property(e => e.Url).HasMaxLength(500);
            entity.Property(e => e.OriginName).HasMaxLength(200);
            entity.Property(e => e.OriginUrl).HasMaxLength(500);
            entity.Property(e => e.LocationName).HasMaxLength(200);
            entity.Property(e => e.LocationUrl).HasMaxLength(500);
            entity.Property(e => e.EpisodeUrl).HasMaxLength(4000);

        });
    }
}