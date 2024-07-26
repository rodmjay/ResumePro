#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.AI.Entities;
using ResumePro.Core.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.AI.Services;

public interface IChatGptService : IService<ApiKey>
{
    Task<ChatResult> Professionalize(int organizationId, int userId, ChatOptions options);
}