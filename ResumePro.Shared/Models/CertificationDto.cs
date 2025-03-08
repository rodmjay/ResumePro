#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class CertificationDto : ICertification
{
    [JsonIgnore] public int OrganizationId { get; set; }

    public string Name { get; set; }
    public string Body { get; set; }

    [JsonIgnore] public int PersonId { get; set; }

    public int Id { get; set; }
    public DateTime Date { get; set; }
}