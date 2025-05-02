#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Interfaces;

public interface IHighlight
{
    int OrganizationId { get; set; }
    int Id { get; set; }
    int CompanyId { get; set; }
    int PositionId { get; set; }
    int PersonId { get; set; }
    int Order { get; set; }
    string Text { get; set; }
    ObjectState ObjectState { get; set; }
}