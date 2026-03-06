using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> Options) : base(Options) { }


        protected override void OnModelCreating(ModelBuilder Modelbuilder)
        {
            base.OnModelCreating(Modelbuilder);

            var ReaderRoleId = "2d067532-f1f0-4cf1-badf-9b8a0e50e9f3";
            var WriterRoleId = "366db56c-02fa-4b46-b993-de55eaadaa5f";
            var Roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = ReaderRoleId,
                    ConcurrencyStamp= ReaderRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = WriterRoleId,
                    ConcurrencyStamp=WriterRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };

            //Seed Identity Role to the Database
            Modelbuilder.Entity<IdentityRole>().HasData(Roles);
        }
    }
}
