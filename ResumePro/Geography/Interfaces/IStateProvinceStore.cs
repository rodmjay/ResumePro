#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Geography.Models;

namespace ResumePro.Geography.Interfaces;

public interface IStateProvinceStore
{
    Task<List<T>> GetStateProvincesForCountry<T>(string id) where T : StateProvinceOutput;
}