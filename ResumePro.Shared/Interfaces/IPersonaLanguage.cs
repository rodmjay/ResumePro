#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Interfaces;

public interface IPersonaLanguage
{
    int OrganizationId { get; set; }
    int PersonaId { get; set; }
    string Code3 { get; set; }
    int Proficiency { get; set; }
}