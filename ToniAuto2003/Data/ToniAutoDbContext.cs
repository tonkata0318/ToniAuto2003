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
    }
}