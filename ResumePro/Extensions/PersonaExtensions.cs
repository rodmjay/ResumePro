﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Linq.Expressions;
using ResumePro.Core.Queries;
using ResumePro.Entities;
using ResumePro.Shared.Filters;

namespace ResumePro.Extensions;

public static class PersonaExtensions
{
    public static Expression<Func<Persona, bool>> GetExpression(this PersonaFilters filters)
    {
        var predicate = PredicateBuilder.True<Persona>();

        if (!string.IsNullOrWhiteSpace(filters.FirstName))
        {
            predicate = predicate.And(x => x.FirstName == filters.FirstName);
        }

        if (!string.IsNullOrWhiteSpace(filters.LastName))
        {
            predicate = predicate.And(x => x.FirstName == filters.LastName);
        }

        if (!string.IsNullOrWhiteSpace(filters.State))
        {
            predicate = predicate.And(x => x.State == filters.State);
        }

        if (filters.Skills.Any())
        {
            predicate = predicate.And(x => filters.Skills.Intersect(x.Skills.Select(a => a.SkillId)).Any());
        }

        return predicate;
    }
}