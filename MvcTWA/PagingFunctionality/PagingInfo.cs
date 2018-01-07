using MvcTWA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace MvcTWA.PagingFunctionality
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages =>
            (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }

    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tb = new TagBuilder("a");
                tb.MergeAttribute("href", pageUrl(i));
                tb.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tb.AddCssClass("selected");
                    tb.AddCssClass("btn-success");
                }
                tb.AddCssClass("btn btn-default");
                sb.Append(tb.ToString());
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }

    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
