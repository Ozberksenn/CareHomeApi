using CareHomeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CareHomeApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<CareRecipient> CareRecipients => Set<CareRecipient>();
    public DbSet<CareProvider> CareProviders => Set<CareProvider>();
    public DbSet<Bill> Bills => Set<Bill>();
    public DbSet<Note> Notes => Set<Note>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>()
            .HasOne(b => b.Recipient)
            .WithMany(r => r.Bills)
            .HasForeignKey(b => b.RecipientId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Note>()
            .HasOne(n => n.Recipient)
            .WithMany(r => r.Notes)
            .HasForeignKey(n => n.RecipientId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Note>()
            .HasOne(n => n.Provider)
            .WithMany(p => p.Notes)
            .HasForeignKey(n => n.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
