#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ResumePro.IdentityServer.Pages.Account.Manage;

public static class ManageNavPages
{
    public static string Index => "Index";

    public static string Email => "Email";

    public static string ChangePassword => "ChangePassword";


    public static string IndexNavClass(ViewContext viewContext)
    {
        return PageNavClass(viewContext, Index);
    }

    public static string EmailNavClass(ViewContext viewContext)
    {
        return PageNavClass(viewContext, Email);
    }

    public static string ChangePasswordNavClass(ViewContext viewContext)
    {
        return PageNavClass(viewContext, ChangePassword);
    }

    private static string PageNavClass(ViewContext viewContext, string page)
    {
        var activePage = viewContext.ViewData["ActivePage"] as string
                         ?? Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
        return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "bg-ss-dark text-white" : null;
    }
}