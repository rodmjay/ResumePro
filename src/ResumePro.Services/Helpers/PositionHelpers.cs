using System.Linq.Expressions;
using Bespoke.Core.Builders;

namespace ResumePro.Services.Helpers;

public static class PositionHelpers
{
    public static Expression<Func<Position, bool>> GetPredicate(int organizationId, int personId, int companyId)
    {
        var predicate = PredicateBuilder.True<Position>()
            .And(x => x.OrganizationId == organizationId)
            .And(x => x.PersonId == personId)
            .And(x => x.CompanyId == companyId);

        return predicate;
    }

    public static Expression<Func<Position, bool>> GetPredicate(int organizationId, int personId, int companyId,
        int positionId)
    {
        var predicate = GetPredicate(organizationId, personId, companyId)
            .And(x => x.Id == positionId);

        return predicate;
    }
}