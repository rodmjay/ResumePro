﻿#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public class SchoolDto : ISchool
{
    [JsonIgnore] public int Id { get; set; }

    [JsonIgnore] public int PersonaId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Name { get; set; }
}