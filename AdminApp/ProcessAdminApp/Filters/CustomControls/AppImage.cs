using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProcessAdminApp.Filters.CustomControls
{
    public static class AppImage
    {

           public static MvcHtmlString ImageReference(
        this HtmlHelper html,
        String id,
        String altText,
        String imageNameAndExtension)
    {

        var baseUrl = "~\\Images\\";
        String src = String.Concat(baseUrl, imageNameAndExtension);

        var img = new TagBuilder("img");
        if (String.IsNullOrEmpty(id) == false) img.MergeAttribute("id", id);
        if (String.IsNullOrEmpty(altText) == false) img.MergeAttribute("alt", altText);
        img.MergeAttribute("src", src);

        return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));

    }
    }

   
}