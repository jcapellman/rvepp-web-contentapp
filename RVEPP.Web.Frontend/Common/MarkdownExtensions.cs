using Microsoft.AspNetCore.Html;

namespace RVEPP.Web.Frontend.Common
{
    public static class MarkdownExtension
    {
        public static HtmlString ToHtmlString(this string markdown)
            => new(Markdig.Markdown.ToHtml(markdown));
    }
}