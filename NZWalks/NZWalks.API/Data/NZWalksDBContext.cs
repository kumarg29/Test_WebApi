using Microsoft.EntityFrameworkCore;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDBContext : DbContext
    {
        public NZWalksDBContext(DbContextOptions DbcontextOptions) : base(DbcontextOptions)
        {

        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }



        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            // Seed data for difficulties
            var difficties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("facbd018-7490-4719-928e-e23e56763361"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("f4c564fd-bccb-486d-860c-6a56c8c02234"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("2cff180b-f855-4cb4-a228-707ab5081546"),
                    Name = "Hard"
                }
            };

            // Seed Dificulties to the database
            modelbuilder.Entity<Difficulty>().HasData(difficties);



            // Seed Data for Region
            var regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.Parse("9d7b6307-cee7-45f0-8536-708ff4fbfad3"),
                    Name = "Auckland",
                    Code = "AKL",
                    ImageUrl = "https://www.gettyimages.com/photos/auckland"
                },
                 new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    ImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    ImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    ImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    ImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    ImageUrl = null
                },

            };

            modelbuilder.Entity<Region>().HasData(regions);
        }
    }
}




