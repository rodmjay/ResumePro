#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Linq.Expressions;
using ResumePro.Core.Builders;

namespace ResumePro.Extensions;

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

    public static Expression<Func<Position, bool>> GetPredicate(int organizationId, int personId, int companyId, int positionId)
    {
        var predicate = GetPredicate(organizationId, personId, companyId)
            .And(x => x.Id == positionId);

        return predicate;
    }
}