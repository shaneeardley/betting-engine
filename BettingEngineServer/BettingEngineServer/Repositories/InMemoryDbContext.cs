using BettingEngineServer.Classes;
using Microsoft.EntityFrameworkCore;

namespace BettingEngineServer.Repositories
{
    //TODo - at some stage, get a DB context working, and persist data
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