#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Interfaces;

public interface IReference : IPersonId, IId, IName, IPhoneNumber, IOrder, IOrganizationId, IText
{
}