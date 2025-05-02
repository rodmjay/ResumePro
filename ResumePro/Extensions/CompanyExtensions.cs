#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Extensions;

public static class CertificationExtensions
{
    public static void Create()
    {

    }
}

public static class CompanyExtensions
{
    public static Company AddSkills(this Company company, List<int> skills)
    {

        foreach (var availableSkill in skills)
            company.Skills.Add(new CompanySkill
            {
                ObjectState = ObjectState.Added,
                SkillId = availableSkill
            });

        return company;
    }

    public static Company Create(this CompanyOptions options, List<int> personSkills, int organizationId, int personId,
        int nextId)
    {
        var company = new Company
        {
            Id = nextId,
            ObjectState = ObjectState.Added,
            StartDate = options.StartDate,
            EndDate = options.EndDate,
            CompanyName = options.Company,
            Description = options.Description,
            Location = options.Location,
            OrganizationId = organizationId,
            PersonId = personId
        };

        var availableSkills = options.JobSkillIds
            .Where(personSkills.Contains).ToList();

        return company.AddPositions(options.Positions)
            .AddSkills(availableSkills);
    }

    public static Company Update(this Company company, CompanyOptions options)
    {
        company.ObjectState = ObjectState.Modified;
        company.CompanyName = options.Company;
        company.Description = options.Description;
        company.EndDate = options.EndDate;
        company.StartDate = options.StartDate;
        company.Location = options.Location;

        return company
            .UpdatePositions(options.Positions)
            .UpdateSkills(options.JobSkillIds);
    }

    public static Company UpdateSkills(this Company company, List<int> jobSkillIds)
    {
        if (jobSkillIds.Count > 0)
        {
            foreach (var skill in company.Skills) skill.ObjectState = ObjectState.Deleted;
        }

        foreach (var skillId in jobSkillIds)
        {
            var skill = company.Skills.FirstOrDefault(x => x.SkillId == skillId);
            if (skill == null)
                company.Skills.Add(new CompanySkill
                {
                    ObjectState = ObjectState.Added,
                    SkillId = skillId
                });
            else
                skill.ObjectState = ObjectState.Unchanged;
        }

        return company;
    }

    public static Company UpdatePositions(this Company company, List<PositionOptions> positionOptionsList)
    {
        foreach (var position in company.Positions)
        {
            position.ObjectState = ObjectState.Deleted;
        }

        for (int i = 0; i < positionOptionsList.Count; i++)
        {
            var positionOptions = positionOptionsList[i];
            var position = company.Positions.FirstOrDefault(x => x.Id == positionOptions.Id);

            if (position == null)
            {
                company.AddPosition(positionOptions, i);
            }
            else
            {
                position.Update(positionOptions);
            }
        }
        return company;
    }

    public static Company AddPosition(this Company company, PositionOptions positionOptions, int index)
    {
        var position = new Position()
        {
            Id = index + 1,
            StartDate = positionOptions.StartDate,
            EndDate = positionOptions.EndDate,
            ObjectState = ObjectState.Added,
            JobTitle = positionOptions.JobTitle
        };

        for (var j = 0; j < positionOptions.Highlights.Count; j++)
        {
            position.AddHighlight(positionOptions.Highlights[j], j);
        }

        position.UpdateProjects(positionOptions.Projects);

        company.Positions.Add(position);

        return company;
    }

    public static Company AddPositions(this Company company, List<PositionOptions> positionList)
    {
        for (int i = 0; i < positionList.Count; i++)
        {
            company.AddPosition(positionList[i], i);
        }

        return company;
    }
}