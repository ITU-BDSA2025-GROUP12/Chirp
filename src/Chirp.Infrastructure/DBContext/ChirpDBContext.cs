using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Chirp.Core1;

namespace Chirp.Infrastructure.Data;
/// <summary>
/// This class defines the database
/// </summary>
public class ChirpDBContext : IdentityDbContext<Author, IdentityRole<int>, int>
{
    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options)
    {
    }
    /// <summary>
    /// The set of every cheep in the database
    /// </summary>
    public DbSet<Cheep> Cheeps { get; set; }
    /// <summary>
    /// The set of every author in the database
    /// </summary>
    public DbSet<Author> Authors { get; set; }
    /// <summary>
    /// Defines the entities and relationships in the database
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Author
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.FirstName)
                .IsRequired();

            entity.HasIndex(a => a.Email)
                .IsUnique();

            // Follow relationship
            entity.HasMany(a => a.Following)
                .WithMany(a => a.Followers)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorFollow",
                    j => j.HasOne<Author>()
                          .WithMany()
                          .HasForeignKey("FollowingId")
                          .OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne<Author>()
                          .WithMany()
                          .HasForeignKey("FollowerId")
                          .OnDelete(DeleteBehavior.Cascade)
                );
        });
        
        // Cheep
        modelBuilder.Entity<Cheep>(entity =>
        {
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
