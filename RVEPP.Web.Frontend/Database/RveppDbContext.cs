using Microsoft.EntityFrameworkCore;

using RVEPP.Web.Frontend.Database.Tables;
using RVEPP.Web.Frontend.Database.Tables.Base;

namespace RVEPP.Web.Frontend.Database
{
    public class RveppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Content> Content { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseTable &&
                (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        ((BaseTable)entityEntry.Entity).Created = DateTime.Now;
                        ((BaseTable)entityEntry.Entity).Active = true;

                        break;
                }

                ((BaseTable)entityEntry.Entity).Modified = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}