using Elk.AspNetCore;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace Shopia.Dashboard.Temp
{
    public class BaseTagHelperModel2 : FormGroupModel
    {
        public string Label { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Value { get; set; }
    }
    [HtmlTargetElement("custom-input2")]
    public class CustomInput2 : BaseTagHelperModel2
    {
        public InputType Type { set; get; } = InputType.text;

        public string PlaceHolder { get; set; } = "";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            output.PreContent.SetHtmlContent($"<div class='form-group group-{Name.ToLower()} {WrapperClass}'>");
            if (LabelVisibility)
            {
                var lbl = new TagBuilder("label");
                lbl.Attributes.Add("for", Name);
                lbl.Attributes.Add("class", LabelClass);
                lbl.InnerHtml.Append(Label);
                output.Content.AppendHtml(lbl);
            }

            var input = new TagBuilder("input");
            input.Attributes.Add("type", Type.ToString());
            input.Attributes.Add("name", Name);
            input.Attributes.Add("id", Id);
            if (Value != null)
            {
                input.Attributes.Add("value", Value);
            }
            input.Attributes.Add("placeholder", PlaceHolder);
            if (Readonly) input.Attributes.Add("readonly", "readonly");
            if (string.IsNullOrWhiteSpace(Class)) input.Attributes.Add("class", "form-control");
            else input.Attributes.Add("class", Class);
            foreach (var attr in context.AllAttributes.Where(x => x.Name.StartsWith("input-")))
                input.Attributes.Add(attr.Name.Replace("input-", ""), attr.Value.ToString());

            output.Content.AppendHtml(input);
            output.PostContent.SetHtmlContent($"</div>");
        }
    }
}
