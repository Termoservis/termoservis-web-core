using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Termoservis.Helpers.TagHelpers
{
    /// <summary>
    /// The page header tag helper. 
    /// This will append H1 title from ViewBag.
    /// </summary>
    /// <seealso cref="TagHelper" />
    [HtmlTargetElement("page-header", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PageHeaderTagHelper : TagHelper
    {
        /// <summary>
        /// Gets or sets the view context.
        /// </summary>
        /// <value>
        /// The view context.
        /// </value>
        [ViewContext]
        public ViewContext ViewContext { get; set; }


        /// <summary>
        /// Synchronously executes the <see cref="T:Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" /> with the given <paramref name="context" /> and
        /// <paramref name="output" />.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", "row");

            var innerHtml = $"<div class='col pb-3 pt-3'><h1>{HttpUtility.HtmlEncode(this.ViewContext.ViewBag.Title ?? "Page")}</h1></div>";
            
            output.Content.SetHtmlContent(innerHtml);
        }
    }
}