using EverywhereNotes.Models.Entities;
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
        public virtual DbSet<User> Users { get; set; }
    }
}
