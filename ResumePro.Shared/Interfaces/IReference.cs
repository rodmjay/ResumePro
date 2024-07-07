#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Interfaces;

public interface IReference
{
    int PersonaId { get; set; }
    int Id { get; set; }
    string Text { get; set; }
    string Name { get; set; }
    string PhoneNumber { get; set; }
    int Order { get; set; }
    int OrganizationId { get; set; }
}