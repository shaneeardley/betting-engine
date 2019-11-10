using BettingEngineServer.Classes;
using Microsoft.EntityFrameworkCore;

namespace BettingEngineServer.Repositories
{
    public class InMemoryDbContext : DbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options)
        {
            
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Market>  Markets { get; set; }
        public DbSet<Bet> Bets { get; set; }
    }
}