#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Mappings;

public class PersonMapping : Profile
{
    public PersonMapping()
    {
        CreateMap<PersonaDetails, PersonOptions>();
    }
}