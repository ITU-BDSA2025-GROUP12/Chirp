using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.Data;

public class ChirpDBContext : DbContext
{
    public ChirpDBContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) // Command
    {
        // Keys
        modelBuilder.Entity<Author>().HasKey(a => a.AuthorId);
        modelBuilder.Entity<Cheep>().HasKey(c => c.CheepId);

        // Relationships
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Cheeps)
            .WithOne(c => c.Author)
            .HasForeignKey("AuthorId")
            .IsRequired();

        // Simple property constraints
        modelBuilder.Entity<Author>()
            .Property(a => a.Name)
            .IsRequired();

        modelBuilder.Entity<Cheep>()
            .Property(c => c.Text)
            .IsRequired();
    }
}
