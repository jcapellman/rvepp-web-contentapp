using Microsoft.AspNetCore.Mvc.RazorPages;

using RVEPP.Web.Frontend.Database;
using RVEPP.Web.Frontend.Database.Tables;

namespace RVEPP.Web.Frontend.Pages.Base
{
    public class BasePageModel(RveppDbContext dbContext) : PageModel
    {
        protected Content? GetLatestContentByType(Enums.ContentTypes type) =>
            dbContext.Content.Where(a => a.ContentType == type && a.Active).OrderByDescending(a => a.Created).FirstOrDefault();

        protected IEnumerable<Content>? GetContentByType(Enums.ContentTypes type) => [.. dbContext.Content.Where(a => a.ContentType == type && a.Active)];

    }
}