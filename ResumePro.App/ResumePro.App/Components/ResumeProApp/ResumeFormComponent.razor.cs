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
    private Dictionary<int, bool> SkillCheckStates = new();
    private Dictionary<int, bool> JobCheckStates = new();

    private Dictionary<string, Dictionary<string, int>> CategorySkills = new();

    private List<PersonaSkillDto> PersonSkills { get; set; }

    [Inject]
    public ITextController TextController { get; set; }
    
    [Parameter]
    public int PersonId { get; set; }

    [Inject]
    public IPersonSkillsController PersonSkillsController { get; set; }

    [Inject]
    public IJobsController JobsController { get; set; }
    
    public List<JobDetails> JobDetailsList { get; set; }
    
    private async Task LoadJobData()
    {
        JobDetailsList = await JobsController.GetJobs(PersonId);

        foreach (var job in JobDetailsList)
        {
            JobCheckStates[job.Id] = Options.JobIds.Contains(job.Id);
        }
    }
    
    private async Task LoadSkillData()
    {

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


    private async Task RephraseDescription(ResumeOptions resume)
    {
        var result = await TextController.Professionalize(new ChatOptions()
        {
            InputText = resume.Description
        });
        resume.Description = result.OutputText;
    }
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        await LoadSkillData();
        await LoadJobData();
    }
    void ToggleSelectionJob(int jobId)
    {
        if (Options.JobIds.Contains(jobId))
        {
            Options.JobIds.Remove(jobId);
        }
        else
        {
            Options.JobIds.Add(jobId);
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