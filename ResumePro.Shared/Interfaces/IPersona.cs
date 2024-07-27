#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Interfaces;

public interface IPersona : IId, IOrganizationId, IFirstAndLastName, IEmail, IPhoneNumber, ILinkedIn, IGitHub, ICity, IStateId
{
}