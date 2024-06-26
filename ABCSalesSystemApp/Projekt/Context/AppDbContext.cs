using Microsoft.EntityFrameworkCore;
using Projekt.Models.Domain;

namespace Projekt.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Firm> Firms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Login);

                entity.ToTable("user");

                entity.Property(e => e.Login)
                    .HasColumnName("Login");
                entity.Property(e => e.Password)
                    .HasColumnName("Password");
                entity.Property(e => e.Role)
                    .HasConversion<string>()
                    .HasColumnName("Role");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient);

                entity.ToTable("client");

                entity.Property(e => e.IdClient)
                    .HasColumnName("IdClient");
                entity.Property(e => e.ClientType)
                    .HasConversion<string>()
                    .HasColumnName("ClientType");
                entity.Property(e => e.Depreciated)
                    .HasColumnName("Depreciated");
            });

            modelBuilder.Entity<Firm>(entity =>
            {
                entity.HasKey(e => e.IdClient);

                entity.ToTable("firm");

                entity.Property(e => e.IdClient)
                    .HasColumnName("IdClient");
                entity.Property(e => e.Name)
                    .HasColumnName("Name");
                entity.Property(e => e.Address)
                    .HasColumnName("Address");
                entity.Property(e => e.Email)
                    .HasColumnName("Email");
                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("PhoneNumber");
                entity.Property(e => e.KRS)
                    .HasColumnName("KRS");

                entity.HasOne(d => d.Client)
                    .WithOne(d => d.Firm)
                    .HasForeignKey<Firm>(d => d.IdClient);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.IdClient);

                entity.ToTable("person");

                entity.Property(e => e.IdClient)
                    .HasColumnName("IdClient");
                entity.Property(e => e.Name)
                    .HasColumnName("Name");
                entity.Property(e => e.Surname)
                    .HasColumnName("Surname");
                entity.Property(e => e.Address)
                    .HasColumnName("Address");
                entity.Property(e => e.Email)
                    .HasColumnName("Email");
                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("PhoneNumber");
                entity.Property(e => e.PESEL)
                    .HasColumnName("PESEL");

                entity.HasOne(d => d.Client)
                    .WithOne(d => d.Person)
                    .HasForeignKey<Person>(d => d.IdClient);
            });
        }
    }
}
