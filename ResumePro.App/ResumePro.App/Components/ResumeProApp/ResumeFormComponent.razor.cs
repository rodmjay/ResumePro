#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.App.Components.ResumeProApp.Bases;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Components.ResumeProApp;

public partial class ResumeFormComponent : FormComponent<ResumeOptions>
{
    Dictionary<int, bool> SkillCheckStates = new Dictionary<int, bool>();

    private Dictionary<string, Dictionary<string, int>> CategorySkills =
        new Dictionary<string, Dictionary<string, int>>();

    private List<PersonaSkillDto> PersonSkills { get; set; }

    [Parameter]
    public int PersonId { get; set; }

    [Inject]
    public IPersonSkillsController PersonSkillsController { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        PersonSkills = await PersonSkillsController.GetSkills(PersonId);

        var categories = PersonSkills.SelectMany(x => x.Categories).Distinct();


        foreach (var category in categories)
        {
            CategorySkills[category] = new Dictionary<string, int>();
        }

        foreach (var skill in PersonSkills)
        {
            foreach (var category in skill.Categories)
            {
                if (!CategorySkills[category].ContainsKey(skill.Name))
                {
                    CategorySkills[category][skill.Name] = skill.SkillId;
                }
            }
        }

        foreach (var skill in PersonSkills)
        {
            SkillCheckStates[skill.SkillId] = Options.SkillIds.Contains(skill.SkillId);
        }
    }

    void ToggleSelection(int skillId)
    {
        if (Options.SkillIds.Contains(skillId))
        {
            Options.SkillIds.Remove(skillId);
        }
        else
        {
            Options.SkillIds.Add(skillId);
        }
    }
}