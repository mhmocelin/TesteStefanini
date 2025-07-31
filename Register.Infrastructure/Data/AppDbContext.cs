using Microsoft.EntityFrameworkCore;
using Register.Domain.Entities;

namespace Register.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasIndex(p => p.CPF).IsUnique();
            entity.HasOne(p => p.Address)
                  .WithOne(a => a.Person)
                  .HasForeignKey<Address>(a => a.PersonId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Street)
                  .IsRequired()
                  .HasMaxLength(150);

            entity.Property(a => a.Number)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.Property(a => a.Neighborhood)
                  .HasMaxLength(100);

            entity.Property(a => a.City)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(a => a.State)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(a => a.Country)
                  .IsRequired()
                  .HasMaxLength(50);
        });

        base.OnModelCreating(modelBuilder);
    }
}
