using Microsoft.EntityFrameworkCore;

using RVEPP.Web.Frontend.Database.Tables;

namespace RVEPP.Web.Frontend.Database
{
    public class RveppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Content> Content { get; set; }
    }
}