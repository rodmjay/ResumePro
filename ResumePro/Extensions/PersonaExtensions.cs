#region Header Info

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

        return predicate;
    }
}