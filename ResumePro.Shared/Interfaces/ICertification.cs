#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Interfaces;

public interface ICertification
{
    int OrganizationId { get; set; }
    string Name { get; set; }
    string Body { get; set; }
    int PersonaId { get; set; }
    int Id { get; set; }
    DateTime Date { get; set; }
}