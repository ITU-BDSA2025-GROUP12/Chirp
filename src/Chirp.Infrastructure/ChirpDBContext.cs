using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Chirp.Infrastructure;

public class ChirpDBContext : IdentityDbContext<Author, IdentityRole<int>, int>
{
    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options)
    {
    }
    
    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) // Command
    {
        base.OnModelCreating(modelBuilder);
        // Keys
        modelBuilder.Entity<Author>().HasKey(a => a.Id);
        modelBuilder.Entity<Cheep>().HasKey(c => c.CheepId);

        // Relationships
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Cheeps)
            .WithOne(c => c.Author)
            .HasForeignKey(c => c.Id)
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
