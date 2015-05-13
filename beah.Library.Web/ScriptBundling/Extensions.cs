

using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace beah.Library.Web.ScriptBundling
{
    public static class Extensions
    {
        public static IHtmlString RenderScript(this UrlHelper helper, params string[] paths)
        {
            string scripts = System.Web.Optimization.Scripts.Render(paths).ToHtmlString();
            string hostName = HttpContext.Current.Request.Url.Scheme + Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Authority;
            string replaced = Regex.Replace(scripts, "src=\"/", "src=\"" + hostName + "/", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return new HtmlString(replaced);
        }
    }
}
