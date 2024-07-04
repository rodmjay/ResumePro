#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Interfaces;

public interface ISchool
{
    int Id { get; set; }
    int PersonaId { get; set; }
    DateTime StartDate { get; set; }
    DateTime? EndDate { get; set; }
    string Name { get; set; }
}