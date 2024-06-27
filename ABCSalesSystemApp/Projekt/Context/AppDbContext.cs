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
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<ProductContract> ProductContract { get; set; }
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

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct);

                entity.ToTable("product");

                entity.Property(e => e.IdProduct)
                    .HasColumnName("IdProduct");
                entity.Property(e => e.Name)
                    .HasColumnName("Name");
                entity.Property(e => e.Description)
                    .HasColumnName("Description");
                entity.Property(e => e.Version)
                    .HasColumnName("Version");
                entity.Property(e => e.ProductCategory)
                    .HasConversion<string>()
                    .HasColumnName("ProductCategory");
                entity.Property(e => e.AnnualPrice)
                    .HasColumnName("AnnualPrice");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.IdDiscount);

                entity.ToTable("discount");

                entity.Property(e => e.IdDiscount)
                    .HasColumnName("IdDiscount");
                entity.Property(e => e.Name)
                    .HasColumnName("Name");
                entity.Property(e => e.Value)
                    .HasColumnName("Value");
                entity.Property(e => e.DateFrom)
                    .HasColumnName("DateFrom");
                entity.Property(e => e.DateTo)
                    .HasColumnName("DateTo");
            });

            modelBuilder.Entity<ProductContract>(entity =>
            {
                entity.HasKey(e => e.IdContract);

                entity.ToTable("productContract");

                entity.Property(e => e.IdContract)
                    .HasColumnName("IdContract");
                entity.Property(e => e.IdClient)
                    .HasColumnName("IdClient");
                entity.Property(e => e.IdProduct)
                    .HasColumnName("IdProduct");
                entity.Property(e => e.ProductVersion)
                    .HasColumnName("ProductVersion");
                entity.Property(e => e.DateFrom)
                    .HasColumnName("DateFrom");
                entity.Property(e => e.DateTo)
                    .HasColumnName("DateTo");
                entity.Property(e => e.ProductUpdateDescription)
                    .HasColumnName("ProductUpdateDescription");
                entity.Property(e => e.UpdateSupportDuration)
                    .HasColumnName("UpdateSupportExtension");
                entity.Property(e => e.IdDiscount)
                    .HasColumnName("IdDiscount");
                entity.Property(e => e.Value)
                    .HasColumnName("Value");
                entity.Property(e => e.TotalPrice)
                    .HasColumnName("TotalPrice");

                entity.HasOne(e => e.Client)
                    .WithMany(e => e.ProductContracts)
                    .HasForeignKey(e => e.IdClient);

                entity.HasOne(e => e.Product)
                    .WithMany(e => e.ProductContracts)
                    .HasForeignKey(e => e.IdProduct);

                entity.HasOne(e => e.Discount)
                    .WithMany(e => e.ProductContracts)
                    .HasForeignKey(e => e.IdDiscount);
            });
        }
    }
}
