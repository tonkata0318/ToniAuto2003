using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToniAuto2003.Infrastructure.Data
{
    public class ToniAutoDbContext : IdentityDbContext
    {
        public ToniAutoDbContext(DbContextOptions<ToniAutoDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Car>()
                .HasOne(c => c.Category)
                .WithMany()
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Car>()
                .HasOne(s => s.Agent)
                .WithMany()
                .HasForeignKey(s => s.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Car>()
               .HasOne(l => l.Leasing)
               .WithMany()
               .HasForeignKey(l => l.LeasingId)
               .OnDelete(DeleteBehavior.Restrict);

            SeedUsers();
            builder.Entity<IdentityUser>()
               .HasData(GuestUser, AgentUser);

            SeedAgent();
            builder.Entity<Agent>()
                .HasData(Agent);

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

            SeedLeasings();
            builder.Entity<Leasing>()
                .HasData(OneYearLeasing);

            SeedCars();
            builder.Entity<Car>()
                .HasData
                (
                FirstCar,
                SecondCar,
                ThirdCar
                );

            base.OnModelCreating(builder);
        }

        private IdentityUser GuestUser { get; set; } = null!;

        private IdentityUser AgentUser { get; set; } = null!;

        private Agent Agent { get; set; } = null!;

        private Category HatchbackCategory { get; set; } = null!;

        private Category LiftbackCategory { get; set; } = null!;

        private Category StationWagonCategory { get; set; } = null!;

        private Category CoupeCategory { get; set; } = null!;

        private Category ConvertibleCategory { get; set; } = null!;

        private Category LimousineCategory { get; set; } = null!;

        private Category SUVCategory { get; set; } = null!;

        private Category CUVCategory { get; set; } = null!;

        private Car FirstCar { get; set; } = null!;

        private Car SecondCar { get; set; } = null!;

        private Car ThirdCar { get; set; } = null!;

        private Leasing OneYearLeasing { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } =null!;

        public DbSet<Car> Cars { get; set; } = null!;

        public DbSet<Clients> Clients { get; set; } = null!;

        public DbSet<Leasing> Leasings { get; set; } = null!;

        public DbSet<Agent> Agents { get; set; } = null!;

        



        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            AgentUser = new IdentityUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "agent@mail.com",
                NormalizedUserName = "agent@mail.com",
                Email = "agent@mail.com",
                NormalizedEmail = "agent@mail.com"
            };

            AgentUser.PasswordHash =
                 hasher.HashPassword(AgentUser, "agent123");

            GuestUser = new IdentityUser()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "guest@mail.com",
                NormalizedUserName = "guest@mail.com",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com"
            };

            GuestUser.PasswordHash =
            hasher.HashPassword(AgentUser, "guest123");
        }


        private void SeedCategories()
        {
            HatchbackCategory = new Category()
            {
                Id = 1,
                Name = "HatchBack"
            };
            LiftbackCategory = new Category()
            {
                Id = 2,
                Name = "Liftback"
            };
            StationWagonCategory = new Category()
            {
                Id = 3,
                Name = " StationWagon"
            };
            CoupeCategory = new Category()
            { 
                Id=4,
                Name= "Coupe"
            };
            ConvertibleCategory = new Category()
            {
                Id = 5,
                Name = "Convertible"
            };
            LimousineCategory = new Category()
            {
                Id = 6,
                Name = "Limousine"
            };
            SUVCategory = new Category()
            {
                Id = 7,
                Name = "SUV"
            };
            CUVCategory = new Category()
            {
                Id = 8,
                Name = "CUV"
            };
        }
        private void SeedAgent()
        {
            Agent = new Agent()
            {
                Id = 1,
                PhoneNumber = "+359888888888",
                UserId = AgentUser.Id
            };
        }
        private void SeedLeasings()
        {
            OneYearLeasing = new Leasing()
            {
                Id = 1,
                Name = "One Year Leasing",
                AmounthPerMonth = 500,
                Months = 12
            };
        }
        private void SeedCars()
        {
            FirstCar = new Car()
            {
                Id = 1,
                Year = 2006,
                Make = "Volkswagen",
                Model = "Golf 5",
                Price = 5000,
                CategoryId = CoupeCategory.Id,
                AgentId = Agent.Id,
                LeasingId = OneYearLeasing.Id,
                RenterId = GuestUser.Id,
                ImageUrl = "https://www.autocar.co.uk/sites/autocar.co.uk/files/styles/gallery_slide/public/volkswagen-golf-plus-5.jpg?itok=egdDOy3x"
            };
            SecondCar = new Car()
            {
                Id = 2,
                Year = 2008,
                Make = "Volkswagen",
                Model = "Touran",
                Price = 8000,
                CategoryId = SUVCategory.Id,
                AgentId = Agent.Id,
                LeasingId = OneYearLeasing.Id,
                ImageUrl= "https://www.autocar.co.uk/sites/autocar.co.uk/files/styles/gallery_slide/public/volkswagen-touran-6.jpg?itok=tQyjcZ5M"
            };
            ThirdCar = new Car()
            {
                Id = 3,
                Year = 2006,
                Make = "Toyota",
                Model = "Tundra",
                Price = 6000,
                CategoryId = SUVCategory.Id,
                AgentId = Agent.Id,
                LeasingId = OneYearLeasing.Id,
                ImageUrl = "https://www.autocar.co.uk/sites/autocar.co.uk/files/styles/gallery_slide/public/toyota-rav-4-rt-34pan_0.jpg?itok=NJ4NDGzY"
            };
        }
    }
}