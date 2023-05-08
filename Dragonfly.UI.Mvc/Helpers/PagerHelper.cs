using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Dragonfly.UI.Mvc.Helpers
{
    public static class PagerHelper
    {

        public static MvcHtmlString Pager(this AjaxHelper helper, int currentPage, int totalRecords, string targetDiv, int pageSize = 10, string actionName = "Index")
        {
            if (totalRecords > 0)
            {
                var sb = new StringBuilder();
                var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

                //Build the Ajax Options
                var ao = new AjaxOptions {UpdateTargetId = targetDiv};

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

                if (currentPage < (totalPages - 1))
                {
                    //Add the Next Links
                    sb.Append("  ");
                    sb.Append(helper.ActionLink(">", actionName, new { Page = currentPage + 1 }, ao));
                    sb.Append("  ");
                    sb.Append(helper.ActionLink(">>", actionName, new { Page = totalPages }, ao));
                }

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
