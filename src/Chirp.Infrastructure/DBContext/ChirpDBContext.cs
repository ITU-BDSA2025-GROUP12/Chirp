using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Chirp.Core1;


namespace Chirp.Infrastructure.Data;

public class ChirpDBContext : IdentityDbContext<Author, IdentityRole<int>, int>
{
    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options)
    {
    }
    
    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    base.OnModelCreating(modelBuilder);

    // Author
    modelBuilder.Entity<Author>(entity =>
    {
        entity.HasKey(a => a.Id);

        entity.Property(a => a.FirstName)
            .IsRequired();

        // Email should be unique
        entity.HasIndex(a => a.Email)
            .IsUnique();
    });

    // Cheep
    modelBuilder.Entity<Cheep>(entity =>
    {
        // Use Id as the primary key
        entity.HasKey(c => c.Id);

        entity.Property(c => c.Text)
            .IsRequired();

        entity.Property(c => c.TimeStamp)
            .IsRequired();

        entity.HasOne(c => c.Author)
            .WithMany(a => a.Cheeps)
            .HasForeignKey(c => c.AuthorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    });
}


}
