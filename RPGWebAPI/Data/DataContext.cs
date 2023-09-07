using Microsoft.EntityFrameworkCore;
using RPGWebAPI.Models;

namespace RPGWebAPI.Data
{
    // If I want to see a represention of a model in the database, and to be able to query and save characters, etc
    // have to add DbSet of that model.
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        // Another way to set Character DB.
        //public DbSet<Character> Characters => Set<Character>();
        public DbSet<Character> Characters { get; set; }

    }
}
