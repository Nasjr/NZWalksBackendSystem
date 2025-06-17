using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NzIdentityDbContext : IdentityDbContext
    {
        public NzIdentityDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "4acd920c-c6e3-4f55-8b42-2d672880e095";
            var writerRoleId = "c4de7b72-7770-43eb-8cf8-b01127ac7d53";
            
            var roles = new List<IdentityRole> { 
            new IdentityRole
            {
                Id = readerRoleId,
                ConcurrencyStamp = readerRoleId,
                Name = "Reader",
                NormalizedName = "Reader".ToUpper(),

            },
             new IdentityRole
            {
                Id = writerRoleId,
                ConcurrencyStamp = writerRoleId,
                Name = "Writer",
                NormalizedName = "Writer".ToUpper(),

            },

            };

            builder.Entity<IdentityRole>().HasData(roles);

        }

    }
}
