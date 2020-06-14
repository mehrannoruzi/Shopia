using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Shopia.Domain;

namespace Shopia.Dashboard
{
    [HtmlTargetElement("nested-view")]
    public class NestedView : TagHelper
    {
        public string TagId { get; set; }
        public string AddText { get; set; }
        public string EditText { get; set; }
        public string DeleteText { get; set; }
        public List<NestedItem> Items { get; set; }
        public string AddFormHtml { get; set; }
        public string GetItemsUrl { get; set; }
        public string GetEditFormUrl { get; set; }
        public string SubmitEditFormUrl { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("nested-view");
            wrapper.Attributes.Add("id", TagId);
            //var filterRow = new TagBuilder("div");
            //filterRow.AddCssClass("nested-items-filter");
            //var input = new TagBuilder("input");
            //input.AddCssClass("input-search");
            var mainUl = new TagBuilder("ul");
            mainUl.AddCssClass("main-nested-ul");
            TagBuilder Appender(NestedItem currentItem)
            {
                var li = new TagBuilder("li");
                li.Attributes.Add("data-id", currentItem.Id.ToString());
                li.InnerHtml.AppendHtml($"<span>{currentItem.Name}</span>");
                var childs = Items.Where(x => x.ParentId == currentItem.Id).OrderByDescending(x => x.OrderPrority).ToList();
                if (childs.Any())
                {
                    li.InnerHtml.AppendHtml($"<ul class='submenu'>");
                    foreach (var child in childs)
                        li.InnerHtml.AppendHtml(Appender(child));
                    li.InnerHtml.AppendHtml($"</ul>");
                }
                return li;
            }
            foreach (var item in Items.Where(x => x.ParentId == null).OrderByDescending(x => x.OrderPrority).ToList())
                mainUl.InnerHtml.AppendHtml(Appender(item));
            wrapper.InnerHtml.AppendHtml(mainUl);
            output.Content.AppendHtml(wrapper);
        }
    }
}
