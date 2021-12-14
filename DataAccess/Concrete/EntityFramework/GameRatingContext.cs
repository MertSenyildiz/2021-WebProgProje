using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class GameRatingContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=GameRating;UserId=postgres;Password=Mert.123");

        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
