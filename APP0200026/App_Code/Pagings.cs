using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DomainModel.Controls;
using System.Collections.Specialized;
using System.Reflection;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using System.ComponentModel;
using DomainModel;

namespace APP0200026.App_Code
{
    public class Pagings
    {
        public static string PageLinks_Cms(String Title, int currentPage, int totalPages, Func<int, string> URL)
        {
            if (totalPages <= 1) return "";
            StringBuilder result = new StringBuilder();
            int MinPage = (currentPage - Globals.PageRange) > 0 ? currentPage - Globals.PageRange : 1;
            int MaxPage = (currentPage + Globals.PageRange) > totalPages ? totalPages : currentPage + Globals.PageRange;
            TagBuilder tag;

            result.AppendLine("<ul class='pagination pagination-sm no-margin pull-right'>");
            result.AppendLine("<li><span>" + Title + "</span></li>");
            if (currentPage <= Globals.PageRange)
            {
                MaxPage = (2 * Globals.PageRange + 1 > totalPages) ? totalPages : 2 * Globals.PageRange + 1;
            }
            if (currentPage > totalPages - Globals.PageRange)
            {
                MinPage = (totalPages - 2 * Globals.PageRange < 1) ? 1 : totalPages - 2 * Globals.PageRange;
            }
            if (MinPage > 1)
            {
                tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", URL(1));
                tag.InnerHtml = "&laquo;&laquo;";
                result.AppendLine("<li>" + tag.ToString() + "</li>");
            }

            if (MinPage < currentPage)
            {
                tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", URL(currentPage - 1));
                tag.InnerHtml = "&laquo;";
                result.AppendLine("<li>" + tag.ToString() + "</li>");
            }
            for (int i = MinPage; i <= MaxPage; i++)
            {
                if (i == currentPage)
                {
                    result.AppendLine(String.Format("<li><span>{0}</span></li>", i));
                }
                else
                {
                    tag = new TagBuilder("a"); // Construct an <a> tag 
                    tag.MergeAttribute("href", URL(i));
                    tag.InnerHtml = i.ToString();
                    result.AppendLine("<li>" + tag.ToString() + "</li>");
                }
            }
            if (MaxPage > currentPage)
            {
                tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", URL(currentPage + 1));
                tag.InnerHtml = "&raquo;";
                result.AppendLine("<li>" + tag.ToString() + "</li>");
            }
            if (MaxPage < totalPages)
            {
                tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", URL(totalPages));
                tag.InnerHtml = "&raquo;&raquo;";
                result.AppendLine("<li>" + tag.ToString() + "</li>");
            }
            result.AppendLine("<li>");
            result.AppendLine("<span style=\"padding: 5px 10px;\"><select onChange='if(this.selectedIndex!=-1) self.location=this.options[this.selectedIndex].value'>");
            for (int i = 1; i <= totalPages; i++)
            {
                if (i == currentPage)
                {
                    result.AppendLine("<option value='" + URL(i) + "' selected>Trang " + i + "</option>");
                }
                else
                {
                    result.AppendLine("<option value='" + URL(i) + "'>Trang " + i + "</option>");
                }
            }
            result.AppendLine("</select></span>");
            result.AppendLine("</li>");
            result.AppendLine("</ul>");
            return result.ToString();
        }
        public static string PageLinks_Home(String Title, int currentPage, int totalPages, Func<int, string> URL)
        {
            if (totalPages <= 1) return "";
            StringBuilder result = new StringBuilder();
            int MinPage = (currentPage - Globals.PageRange) > 0 ? currentPage - Globals.PageRange : 1;
            int MaxPage = (currentPage + Globals.PageRange) > totalPages ? totalPages : currentPage + Globals.PageRange;
            TagBuilder tag;

            result.AppendLine("<ul class='pagination mb-0 pagination-shop justify-content-center justify-content-md-start'>");
            if (currentPage <= Globals.PageRange)
            {
                MaxPage = (2 * Globals.PageRange + 1 > totalPages) ? totalPages : 2 * Globals.PageRange + 1;
            }
            if (currentPage > totalPages - Globals.PageRange)
            {
                MinPage = (totalPages - 2 * Globals.PageRange < 1) ? 1 : totalPages - 2 * Globals.PageRange;
            }
            if (MinPage > 1)
            {
                tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", URL(1));
                tag.InnerHtml = "&larr;&larr;";
                tag.AddCssClass("page-link");
                result.AppendLine("<li class='page-item'>" + tag.ToString() + "</li>");
            }

            if (MinPage < currentPage)
            {
                tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", URL(currentPage - 1));
                tag.InnerHtml = "&larr;";
                tag.AddCssClass("page-link");
                result.AppendLine("<li class='page-item'>" + tag.ToString() + "</li>");
            }
            for (int i = MinPage; i <= MaxPage; i++)
            {
                if (i == currentPage)
                {
                    result.AppendLine(String.Format("<li class='page-item'><a class='page-link current' href='#'>{0}</a></li>", i));
                }
                else
                {
                    tag = new TagBuilder("a"); // Construct an <a> tag 
                    tag.MergeAttribute("href", URL(i));
                    tag.InnerHtml = i.ToString();
                    tag.AddCssClass("page-link");
                    result.AppendLine("<li class='page-item'>" + tag.ToString() + "</li>");
                }
            }
            if (MaxPage > currentPage)
            {
                tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", URL(currentPage + 1));
                tag.InnerHtml = "&rarr;";
                tag.AddCssClass("page-link");
                result.AppendLine("<li class='page-item'>" + tag.ToString() + "</li>");
            }
            if (MaxPage < totalPages)
            {
                tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", URL(totalPages));
                tag.InnerHtml = "&rarr;&rarr;";
                tag.AddCssClass("page-link");
                result.AppendLine("<li class='page-item'>" + tag.ToString() + "</li>");
            }
            result.AppendLine("</ul>");
            return result.ToString();
        }
        public static string PageLinks_Wap(String Title, int currentPage, int totalPages, Func<int, string> URL)
        {
            if (totalPages <= 1) return "";
            StringBuilder result = new StringBuilder();
            int MinPage = (currentPage - Globals.PageRange) > 0 ? currentPage - Globals.PageRange : 1;
            int MaxPage = (currentPage + Globals.PageRange) > totalPages ? totalPages : currentPage + Globals.PageRange;
            TagBuilder tag;

            result.AppendLine("<ul class='text-center pager'>");
            result.AppendLine("<li class='disabled'><a class='active'>" + Title + "</a></li>");

            if (currentPage <= Globals.PageRange)
            {
                MaxPage = (2 * Globals.PageRange + 1 > totalPages) ? totalPages : 2 * Globals.PageRange + 1;
            }
            if (currentPage > totalPages - Globals.PageRange)
            {
                MinPage = (totalPages - 2 * Globals.PageRange < 1) ? 1 : totalPages - 2 * Globals.PageRange;
            }
            if (MinPage > 1)
            {
                tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", URL(1));
                tag.InnerHtml = "&lt;&lt;";
                result.AppendLine("<li>" + tag.ToString() + "</li>");
            }

            if (MinPage < currentPage)
            {
                tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", URL(currentPage - 1));
                tag.InnerHtml = "&lt;";
                result.AppendLine("<li>" + tag.ToString() + "</li>");
            }
            for (int i = MinPage; i <= MaxPage; i++)
            {
                if (i == currentPage)
                {
                    result.AppendLine(String.Format("<li class='disabled'><a class='active'>{0}</a></li>", i));
                }
                else
                {
                    tag = new TagBuilder("a"); // Construct an <a> tag 
                    tag.MergeAttribute("href", URL(i));
                    tag.InnerHtml = i.ToString();
                    result.AppendLine("<li>" + tag.ToString() + "</li>");
                }
            }
            if (MaxPage > currentPage)
            {
                tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", URL(currentPage + 1));
                tag.InnerHtml = "&gt;";
                result.AppendLine("<li>" + tag.ToString() + "</li>");
            }
            if (MaxPage < totalPages)
            {
                tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", URL(totalPages));
                tag.InnerHtml = "&gt;&gt;";
                result.AppendLine("<li>" + tag.ToString() + "</li>");
            }
            result.AppendLine("</ul>");
            return result.ToString();
        }
    }
}