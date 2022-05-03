using EverywhereNotes.Models;
using Microsoft.EntityFrameworkCore;

namespace EverywhereNotes.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) 
        {
        }

        public virtual DbSet<Note> Notes { get; set; }
    }
}
