#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ResumePro.IdentityServer.TagHelpers;

[HtmlTargetElement("left-column", TagStructure = TagStructure.NormalOrSelfClosing)]
public class LeftColumnHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.AddClass("col-6", HtmlEncoder.Default);
        output.AddClass("col-offset-3", HtmlEncoder.Default);
        //output.AddClass("col-lg-6", HtmlEncoder.Default);
        output.AddClass("align-items-center", HtmlEncoder.Default);
        output.AddClass("m-auto", HtmlEncoder.Default);
    }
}