#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using ResumePro.App.Components.Bases;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Components.Companies;

public partial class JobFormComponent : FormComponent<CompanyOptions>
{
    [Inject]
    public ITextController TextController { get; set; }
    
    Dictionary<int, bool> SkillCheckStates = new();

    private Dictionary<string, Dictionary<string, int>> CategorySkills = new();

    private Dictionary<string, int> OtherSkills = new();

    [Inject]
    public IPersonSkillsController PersonSkillsController { get; set; }

    [Parameter]
    public int PersonId { get; set; }
    
    private List<PersonaSkillDto> PersonSkills { get; set; }

    void ToggleSelection(int skillId)
    {
        if (Options.JobSkillIds.Contains(skillId))
        {
            Options.JobSkillIds.Remove(skillId);
        }
        else
        {
            Options.JobSkillIds.Add(skillId);
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        
        PersonSkills = await PersonSkillsController.GetSkills(PersonId);

        IEnumerable<string> categories = PersonSkills.SelectMany(x => x.Categories).Distinct();


        foreach (string category in categories)
        {
            CategorySkills[category] = new Dictionary<string, int>();
        }

        foreach (PersonaSkillDto skill in PersonSkills)
        {
            foreach (string category in skill.Categories)
            {
                if (!CategorySkills[category].ContainsKey(skill.Name))
                {
                    CategorySkills[category][skill.Name] = skill.SkillId;
                }
            }
        }

        foreach (PersonaSkillDto skill in PersonSkills)
        {
            SkillCheckStates[skill.SkillId] = Options.JobSkillIds.Contains(skill.SkillId);
        }

        OtherSkills = PersonSkills.Where(x => x.Categories.Length == 0)
            .ToDictionary(x => x.Name, x => x.SkillId);
    }


    private void MoveHighlightUp(int index)
    {
        if (index > 0 && index < Options.HighlightOptions.Count)
        {
            HighlightOptions item = Options.HighlightOptions[index];
            Options.HighlightOptions.RemoveAt(index);
            Options.HighlightOptions.Insert(index - 1, item);
        }
    }

    private void MoveProjectHighlightUp(ProjectOptions projectOptions, int index)
    {
        if (index > 0 && index < projectOptions.HighlightOptions.Count)
        {
            HighlightOptions item = projectOptions.HighlightOptions[index];
            projectOptions.HighlightOptions.RemoveAt(index);
            projectOptions.HighlightOptions.Insert(index - 1, item);
        }
    }

    private void MoveProjectHighlightDown(ProjectOptions projectOptions, int index)
    {
        if (index >= 0 && index < projectOptions.HighlightOptions.Count - 1)
        {
            HighlightOptions item = projectOptions.HighlightOptions[index];
            projectOptions.HighlightOptions.RemoveAt(index);
            projectOptions.HighlightOptions.Insert(index + 1, item);
        }
    }

    private void MoveProjectUp(int index)
    {
        if (index >= 0 && index < Options.ProjectOptions.Count - 1)
        {
            ProjectOptions item = Options.ProjectOptions[index];
            Options.ProjectOptions.RemoveAt(index);
            Options.ProjectOptions.Insert(index + 1, item);
        }
    }

    private void MoveProjectDown(int index)
    {
        if (index >= 0 && index < Options.ProjectOptions.Count - 1)
        {
            ProjectOptions item = Options.ProjectOptions[index];
            Options.ProjectOptions.RemoveAt(index);
            Options.ProjectOptions.Insert(index + 1, item);
        }
    }

    private void MoveHighlightDown(int index)
    {
        if (index >= 0 && index < Options.HighlightOptions.Count - 1)
        {
            HighlightOptions item = Options.HighlightOptions[index];
            Options.HighlightOptions.RemoveAt(index);
            Options.HighlightOptions.Insert(index + 1, item);
        }
    }

    private void AddHighlight()
    {
        Options.HighlightOptions.Add(new HighlightOptions());
    }

    private async Task Rephrase(HighlightOptions highlight)
    {
        ChatResult result = await TextController.Professionalize(new ChatOptions()
        {
            InputText = highlight.Text
        });
        highlight.Text = result.OutputText;
    }

    private async Task Rephrase(CompanyOptions company)
    {
        ChatResult result = await TextController.Professionalize(new ChatOptions()
        {
            InputText = company.Description
        });
        company.Description = result.OutputText;
    }

    private async Task Rephrase(ProjectOptions project)
    {
        ChatResult result = await TextController.Professionalize(new ChatOptions()
        {
            InputText = project.Description
        });
        project.Description = result.OutputText;
    }


    private void RemoveHighlight(HighlightOptions highlight)
    {
        Options.HighlightOptions.Remove(highlight);
    }

    private void AddProjectHighlight(ProjectOptions options)
    {
        options.HighlightOptions.Add(new HighlightOptions());
    }

    private void RemoveProjectHighlight(ProjectOptions project, HighlightOptions options)
    {
        project.HighlightOptions.Remove(options);
    }

    private void AddProject()
    {
        Options.ProjectOptions.Add(new ProjectOptions());
    }

    private void RemoveProject(ProjectOptions project)
    {
        Options.ProjectOptions.Remove(project);
    }
}