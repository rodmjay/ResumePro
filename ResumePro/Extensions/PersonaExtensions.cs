#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Linq.Expressions;
using ResumePro.Shared.Filters;
using TechValley.Core.Builders;

namespace ResumePro.Extensions;

public static class PersonaExtensions
{
    public static Persona AddCertificate(this Persona persona, CertificationOptions options, Func<Task<int>> newId)
    {
        var id = newId().Result;

        var certification = new Certification
        {
            ObjectState = ObjectState.Added,
            Id = id,
            Body = options.Body,
            OrganizationId = persona.OrganizationId,
            PersonId = id,
            Date = options.Date.Value,
            Name = options.Name
        };

        persona.Certifications.Add(certification);

        return persona;
    }

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
        {
            predicate = predicate.And(x => filters.Skills.All(requiredSkillId =>
                x.Skills.Select(a => a.SkillId).Contains(requiredSkillId)));
        }

        return predicate;
    }
}