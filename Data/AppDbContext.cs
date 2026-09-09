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
    public DbSet<UtilityBillType> UtilityBillTypes => Set<UtilityBillType>();
    public DbSet<Bill> Bills => Set<Bill>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<Meal> Meals => Set<Meal>();
    public DbSet<User> Users => Set<User>();
    public DbSet<DutyRotationEntry> DutyRotationEntries => Set<DutyRotationEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>()
            .HasOne(b => b.UtilityBillType)
            .WithMany(t => t.Bills)
            .HasForeignKey(b => b.UtilityBillTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UtilityBillType>().HasData(
            new UtilityBillType { Id = 1, Name = "Su", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new UtilityBillType { Id = 2, Name = "Elektrik", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new UtilityBillType { Id = 3, Name = "Doğalgaz", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );

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

        modelBuilder.Entity<Meal>()
            .HasOne(m => m.Provider)
            .WithMany(p => p.Meals)
            .HasForeignKey(m => m.ProviderId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<DutyRotationEntry>()
            .HasOne(d => d.CareProvider)
            .WithMany(p => p.DutyRotationEntries)
            .HasForeignKey(d => d.CareProviderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DutyRotationEntry>()
            .HasIndex(d => d.CareProviderId)
            .IsUnique();
    }
}
