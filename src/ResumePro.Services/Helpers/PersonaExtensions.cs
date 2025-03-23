using System.Linq.Expressions;
using Bespoke.Core.Builders;
using ResumePro.Shared.Filters;

namespace ResumePro.Services.Helpers;

public static class PersonaExtensions
{
    public static Expression<Func<Persona, bool>> GetExpression(this PersonaFilters filters)
    {
        var predicate = PredicateBuilder.True<Persona>();

        if (!string.IsNullOrWhiteSpace(filters.FirstName))
            predicate = predicate.And(x => x.FirstName == filters.FirstName);

        if (!string.IsNullOrWhiteSpace(filters.LastName))
            predicate = predicate.And(x => x.FirstName == filters.LastName);

        if (filters.States.Any())
            predicate = predicate.And(x => filters.States.Contains(x.StateId));

        //if (filters.Skills.Any())
        //    predicate = predicate.And(x => filters.Skills.Intersect(x.Skills.Select(a => a.SkillId)).Any());

        if (filters.Skills.Any())
            predicate = predicate.And(x => filters.Skills.All(requiredSkillId =>
                x.Skills.Select(a => a.SkillId).Contains(requiredSkillId)));

        return predicate;
    }
}