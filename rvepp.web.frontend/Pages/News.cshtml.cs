using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using RVEPP.Web.Frontend.Database;
using RVEPP.Web.Frontend.Database.Tables;

namespace RVEPP.Web.Frontend.Pages
{
    public class NewsModel(RveppDbContext dbContext) : PageModel
    {
        public List<Content>? NewsPosts { get; private set; }

        public void OnGet()
        {
            NewsPosts = [.. dbContext.Content.Where(a => a.ContentType == Enums.ContentTypes.News).OrderByDescending(a => a.Created)];
        }
    }
}