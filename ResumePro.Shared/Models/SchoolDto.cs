﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion


#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared.Models;

public class SchoolDto : ISchool
{
    public string DisplayEndDate
    {
        get
        {
            if (EndDate == null) return "Present";

            return EndDate.Value.ToShortDateString();
        }
    }

    public int Id { get; set; }

    [JsonIgnore] public int PersonaId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public string Name { get; set; }
}