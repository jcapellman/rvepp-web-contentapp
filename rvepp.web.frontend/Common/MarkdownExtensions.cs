using Microsoft.AspNetCore.Html;

namespace rvepp.web.frontend.Common
{
    public static class MarkdownExtension
    {
        public static HtmlString ToHtmlString(this string markdown)
            => new(Markdig.Markdown.ToHtml(markdown));
    }
}