#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Interfaces;

public interface IProject : IId, IOrder, IName, IDescription, ICompanyId, IBudget
{
}