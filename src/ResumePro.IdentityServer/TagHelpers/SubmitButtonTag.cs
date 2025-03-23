#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ResumePro.IdentityServer.TagHelpers;

[HtmlTargetElement("submit-button", Attributes = "name", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SubmitButtonTag : TagHelper
{
    public string Name { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.Add("type", "submit");
        output.AddClass("btn", HtmlEncoder.Default);
        output.AddClass("btn-primary", HtmlEncoder.Default);
        output.AddClass("btn-lg", HtmlEncoder.Default);
        output.AddClass("w-100", HtmlEncoder.Default);
        output.AddClass("mt-3", HtmlEncoder.Default);

        output.Content.SetContent(Name);
    }
}