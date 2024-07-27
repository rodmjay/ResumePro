#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Linq.Expressions;
using ResumePro.Core.Builders;
using ResumePro.Languages.Entities;
using ResumePro.Languages.Models;

namespace ResumePro.Languages.Extensions;

public static class LanguageFilterExtensions
{
    public static Expression<Func<Language, bool>> GetExpression(this LanguageFilters filters)
    {
        var expr = PredicateBuilder.True<Language>();

        if (filters.Name != null) expr.And(x => x.Name.Contains(filters.Name));

        return expr;
    }
}