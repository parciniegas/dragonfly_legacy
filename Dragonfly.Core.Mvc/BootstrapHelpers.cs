using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Dragonfly.Core.Mvc
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
            spanBuilder.MergeAttribute("class", $"glyphicon {icon}");
            spanBuilder.MergeAttribute("aria-hidden", "true");
            var span = spanBuilder.ToString();

            anchorBuilder.InnerHtml = $"{span} {text}";
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

        public static MvcHtmlString Pager(this AjaxHelper helper, int currentPage, int totalRecords, string targetDiv, int pageSize = 10, string actionName = "Index")
        {
            if (totalRecords > 0)
            {
                var sb = new StringBuilder();
                var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

                //Build the Ajax Options
                var ao = new AjaxOptions { UpdateTargetId = targetDiv };

                if (currentPage > 0)
                {
                    //Add the Back Links
                    sb.Append(helper.ActionLink("<<", actionName, new { Page = 0 }, ao));
                    sb.Append("  ");
                    sb.Append(helper.ActionLink("<", actionName, new { Page = currentPage - 1 }, ao));
                    sb.Append("  ");
                }

                //Add the Page Number
                sb.Append("Page " + (currentPage + 1) + " of " + (totalPages));

                if (currentPage >= (totalPages - 1))
                    return MvcHtmlString.Create(sb.ToString());

                //Add the Next Links
                sb.Append("  ");
                sb.Append(helper.ActionLink(">", actionName, new { Page = currentPage + 1 }, ao));
                sb.Append("  ");
                sb.Append(helper.ActionLink(">>", actionName, new { Page = totalPages }, ao));

                return MvcHtmlString.Create(sb.ToString());
            }
            else
            {
                //Don't return anything for the pager if we do not have any records
                return MvcHtmlString.Create("");
            }
        }

    }
}

