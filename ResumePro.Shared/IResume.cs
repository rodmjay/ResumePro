#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public interface IResume
{
    int PersonaId { get; set; }
    int Id { get; set; }
    string ShortName { get; set; }
    string Description { get; set; }
}