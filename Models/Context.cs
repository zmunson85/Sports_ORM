using Microsoft.EntityFrameworkCore;
using SportsORM.Models;


namespace SportsORM.Models {
    public class Context : DbContext {
        public Context(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlite("Filename=SportsORM.db");
        }
        public DbSet<League> Leagues {get;set;}
        public DbSet<Team> Teams {get;set;}
        public DbSet<Player> Players {get;set;}
        public DbSet<PlayerTeam> PlayerTeams {get;set;}
    }
}