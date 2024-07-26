#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Models;

namespace ResumePro.Shared.Interfaces;

public interface ITextController
{
    Task<ChatResult> Professionalize([FromBody] ChatOptions options);
}