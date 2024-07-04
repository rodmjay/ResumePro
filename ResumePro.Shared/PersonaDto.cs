#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared;

public class PersonaDto : IPersona
{
    [JsonIgnore] public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string LinkedIn { get; set; }
    public string GitHub { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int OrganizationId { get; set; }
    public int Skills { get; set; }
    public int Resumes { get; set; }
}