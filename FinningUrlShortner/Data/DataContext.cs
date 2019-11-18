using FinningUrlShortner.Models;
using Microsoft.EntityFrameworkCore;

namespace FinningUrlShortner.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ShortUrl> ShortUrls { get; set; }
    }
}
