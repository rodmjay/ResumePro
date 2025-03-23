#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Duende.IdentityServer.Models;

namespace ResumePro.IdentityServer.Home;

public class ErrorViewModel
{
    public ErrorViewModel()
    {
    }

    public ErrorViewModel(string error)
    {
        Error = new ErrorMessage {Error = error};
    }

    public ErrorMessage Error { get; set; }
}