﻿#region Header

// /*

// Author: Rod Johnson, Architect, rodmjay@gmail.com
// */

#endregion

using System.Linq.Expressions;
using ResumePro.Core.Queries;
using ResumePro.Languages.Entities;
using ResumePro.Languages.Models;

namespace ResumePro.Languages.Extensions
{
    public static class LanguageFilterExtensions
    {
        public static Expression<Func<Language, bool>> GetExpression(this LanguageFilters filters)
        {
            var expr = PredicateBuilder.True<Language>();

            if (filters.Name != null)
            {
                expr.And(x => x.Name.Contains(filters.Name));
            }

            return expr;
        }
    }
}