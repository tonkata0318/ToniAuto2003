using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToniAuto2003.Data
{
    public class ToniAutoDbContext : IdentityDbContext
    {
        public ToniAutoDbContext(DbContextOptions<ToniAutoDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Catalogue>()
                .HasOne(c=>c.Category)
                .WithMany(c=>c.Catalogues)
                .HasForeignKey(c=>c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Car>()
                .HasOne(c=>c.Category)
                .WithMany()
                .HasForeignKey(c=>c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Car>()
                .HasOne(s=>s.Owner)
                .WithMany()
                .HasForeignKey(s=>s.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Car>()
               .HasOne(l=>l.Leasing)
               .WithMany()
               .HasForeignKey(l=>l.LeasingId)
               .OnDelete(DeleteBehavior.Restrict);

            SeedUsers();
            builder.Entity<IdentityUser>()
               .HasData(SalesManUser, GuestUser);

            SeedCategories();
            builder.Entity<Category>()
                .HasData
                (
                HatchbackCategory,
                LiftbackCategory,
                StationWagonCategory,
                CoupeCategory,
                ConvertibleCategory,
                LimousineCategory,
                SUVCategory,
                CUVCategory
                );

            base.OnModelCreating(builder);
        }

        private IdentityUser SalesManUser { get; set; }

        private IdentityUser GuestUser { get; set; }

        private Category HatchbackCategory { get; set; }

        private Category LiftbackCategory { get; set; }

        private Category StationWagonCategory { get; set; }

        private Category CoupeCategory { get; set; }

        private Category ConvertibleCategory { get; set; }

        private Category LimousineCategory { get; set; }

        private Category SUVCategory { get; set; }

        private Category CUVCategory { get; set; }


        public DbSet<Catalogue> Catalogues { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Clients> Clients { get; set; }

        public DbSet<Leasing> Leasings { get; set; }



        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            SalesManUser = new IdentityUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "agent@mail.com",
                NormalizedUserName = "agent@mail.com",
                Email = "agent@mail.com",
                NormalizedEmail = "agent@mail.com"
            };

            SalesManUser.PasswordHash =
                 hasher.HashPassword(SalesManUser, "agent123");

            GuestUser = new IdentityUser()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "guest@mail.com",
                NormalizedUserName = "guest@mail.com",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com"
            };

            GuestUser.PasswordHash =
            hasher.HashPassword(GuestUser, "guest123");
        }
        private void SeedCategories()
        {
            HatchbackCategory = new Category()
            {
                Id = 1,
                Name = "HatchBack cars"
            };
            LiftbackCategory = new Category()
            {
                Id = 2,
                Name = "Liftback cars"
            };
            StationWagonCategory = new Category()
            {
                Id = 3,
                Name = " StationWagon cars"
            };
            CoupeCategory = new Category()
            { 
                Id=4,
                Name= "CoupeCategory cars"
            };
            ConvertibleCategory = new Category()
            {
                Id = 5,
                Name = "ConvertibleCategory cars"
            };
            LimousineCategory = new Category()
            {
                Id = 6,
                Name = "LimousineCategory cars"
            };
            SUVCategory = new Category()
            {
                Id = 7,
                Name = "SUVCategory cars"
            };
            CUVCategory = new Category()
            {
                Id = 8,
                Name = "CUVCategory cars"
            };
        }
    }
}