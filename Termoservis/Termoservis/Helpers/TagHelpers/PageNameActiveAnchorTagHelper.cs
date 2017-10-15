using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Termoservis.Helpers.TagHelpers
{
    /// <summary>
    /// Makes the anchor elements `active` when on same page name as requested in link.
    /// </summary>
    /// <seealso cref="TagHelper" />
    [HtmlTargetElement("a")]
    public class PageNameActiveAnchorTagHelper : TagHelper
    {
        protected const string PageNameActiveLinkAttributeName = "page-name-active";

        /// <summary>
        /// Gets or sets the view context.
        /// </summary>
        /// <value>
        /// The view context.
        /// </value>
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active on page name.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active on page name; otherwise, <c>false</c>.
        /// </value>
        [HtmlAttributeName(PageNameActiveLinkAttributeName)]
        public bool IsActiveOnPageName { get; set; }


        /// <summary>
        /// Synchronously executes the <see cref="T:Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" /> with the given <paramref name="context" /> and
        /// <paramref name="output" />.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Ignore if we don't have view context or link is not controller active
            if (this.ViewContext == null || !this.IsActiveOnPageName)
                return;

            // Ignore if requested page name isn't the same as current page name
            var helper = new UrlHelper(this.ViewContext);
            var requestedPageName = helper.Page(context.AllAttributes["asp-page"].Value.ToString());
            var currentPageName = helper.Page(this.ViewContext.RouteData.Values["page"].ToString());
            if (currentPageName != requestedPageName)
                return;

            // Append `active` class
            var activeTagBuilder = new TagBuilder("a");
            activeTagBuilder.MergeAttribute("class", "active");
            output.MergeAttributes(activeTagBuilder);
        }
    }
}
