﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared;

public class CertificationDto : ICertification
{
    [JsonIgnore]
    public int OrganizationId { get; set; }
    public string Name { get; set; }
    public string Body { get; set; }

    [JsonIgnore]
    public int PersonaId { get; set; }
    public int Id { get; set; }
    public DateTime Date { get; set; }
}