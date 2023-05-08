using System.Web.Mvc;

namespace Dragonfly.UI.Mvc.Helpers
{
    public static class BootstrapHelpers
    {
        public static MvcHtmlString IconActionLink(this HtmlHelper helper, string icon, string action, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            
            var builder1 = new TagBuilder("a");
             if (htmlAttributes != null)
                builder1.MergeAttributes(attributes);
            
            builder1.MergeAttribute("href", action);

            var builder2 = new TagBuilder("span");
            builder2.MergeAttribute("class", icon); 
            builder2.MergeAttribute("aria-hidden", "true");

            var span = builder2.ToString();
            builder1.InnerHtml = span;
            return new MvcHtmlString(builder1.ToString());
        }

        public static MvcHtmlString ButtonActionLink(this HtmlHelper helper, string action, string text, string icon,
            object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var anchorBuilder = new TagBuilder("a");
            if (htmlAttributes != null)
                anchorBuilder.MergeAttributes(attributes);
            anchorBuilder.MergeAttribute("href", action);

            var spanBuilder = new TagBuilder("span");
            spanBuilder.MergeAttribute("class", string.Format("glyphicon {0}", icon));
            spanBuilder.MergeAttribute("aria-hidden", "true");
            var span = spanBuilder.ToString();

            anchorBuilder.InnerHtml = string.Format("{0} {1}", span, text);
            return new MvcHtmlString(anchorBuilder.ToString());
        }

        public static MvcHtmlString SubmitButton(this HtmlHelper helper, string name, string text, object htmlAttributes = null)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            var builder = new TagBuilder("input");

            if (htmlAttributes != null)
                builder.MergeAttributes(attributes);

            builder.Attributes.Add("type", "submit");
            builder.Attributes.Add("value", text);
            builder.Attributes.Add("name", name);
            builder.Attributes.Add("id", name);
            builder.AddCssClass("submit");
            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}
