using Chirp.Core;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.Data;

public class ChirpDBContext : DbContext
{
    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options) { }

    private DbSet<Cheep> Cheeps => Set<Cheep>();
    private DbSet<Author> Authors => Set<Author>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Keys
        modelBuilder.Entity<Author>().HasKey(a => a.AuthorId);
        modelBuilder.Entity<Cheep>().HasKey(c => c.CheepId);

        // Relationships
        modelBuilder.Entity<Cheep>()
            .HasOne(c => c.Author)
            .WithMany(a => a.cheeps)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        // Simple property constraints
        modelBuilder.Entity<Author>()
            .Property(a => a.Name)
            .IsRequired();

        modelBuilder.Entity<Cheep>()
            .Property(c => c.Text)
            .IsRequired();
    }
}
