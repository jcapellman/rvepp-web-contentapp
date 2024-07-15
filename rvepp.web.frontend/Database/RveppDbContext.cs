using Microsoft.EntityFrameworkCore;

using rvepp.web.frontend.Database.Tables;

namespace rvepp.web.frontend.Database
{
    public class RveppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Content> Content { get; set; }
    }
}