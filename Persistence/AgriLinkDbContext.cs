using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Persistence;

public class AgriLinkDbContext : IdentityDbContext<User>
{
    public DbSet<Farm> farm { get; set; }
    public DbSet<Address> addresses { get; set; }

    public AgriLinkDbContext(DbContextOptions<AgriLinkDbContext> options)
          : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AgriLinkDbContext).Assembly);

        // Mamibet with address
        modelBuilder.Entity<Farm>()
        .HasOne(e => e.Address)
        .WithOne(d => d.Farm)
        .HasForeignKey<Address>(e => e.FarmId)
        .OnDelete(DeleteBehavior.Cascade);

        // Unqiue mamibet name
        modelBuilder.Entity<Farm>()
         .HasIndex(s => s.FarmName)
         .IsUnique();

         

    }



    }
