#region Header

// /*

// Author: Rod Johnson, Architect, rodmjay@gmail.com
// */

#endregion

using System.Linq.Expressions;
using ResumePro.Core.Queries;
using ResumePro.Geography.Entities;
using ResumePro.Geography.Models;

namespace ResumePro.Geography.Extensions
{
    public static class CountryFiltersExtensions
    {
        public static Expression<Func<Country, bool>> GetExpression(this CountryFilters filters)
        {
            var expr = PredicateBuilder.True<Country>();
            if (filters.Name != null)
            {
                expr.And(x => x.Name.Contains(filters.Name));
            }
            return expr;
        }
    }
}