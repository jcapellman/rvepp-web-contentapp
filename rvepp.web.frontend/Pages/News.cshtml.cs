using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using rvepp.web.frontend.Database;
using rvepp.web.frontend.Database.Tables;

namespace rvepp.web.frontend.Pages
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