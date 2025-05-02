#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.AI.Entities;
using ResumePro.Shared.Models;
using TechValley.Services.Interfaces;

namespace ResumePro.AI.Services;

public interface IChatGptService : IService<ApiKey>
{
    Task<ChatResult> Professionalize(int organizationId, int userId, ChatOptions options);
}