#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Linq.Expressions;
using ResumePro.Core.Queries;
using ResumePro.Shared.Models;

namespace ResumePro.Extensions;

public static class CountryFiltersExtensions
{
    public static Expression<Func<Country, bool>> GetExpression(this CountryFilters filters)
    {
        var expr = PredicateBuilder.True<Country>();
        if (filters.Name != null) expr.And(x => x.Name.Contains(filters.Name));
        return expr;
    }
}