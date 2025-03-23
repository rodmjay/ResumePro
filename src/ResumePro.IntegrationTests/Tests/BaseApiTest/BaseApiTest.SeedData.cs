using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Data.Contexts;
using ResumePro.Domain.Entities;
using ResumePro.Entities;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<List<Skill>> GetSeededSkills()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<Skill>().ToListAsync();
        }
        
        protected async Task<List<School>> GetSeededSchools()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<School>().ToListAsync();
        }
        
        protected async Task<List<Degree>> GetSeededDegrees()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<Degree>().ToListAsync();
        }
        
        protected async Task<List<Persona>> GetSeededPersonas()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<Persona>().ToListAsync();
        }
        
        protected async Task<List<PersonaSkill>> GetSeededPersonaSkills()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<PersonaSkill>().ToListAsync();
        }
        
        protected async Task<List<PersonaLanguage>> GetSeededPersonaLanguages()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<PersonaLanguage>().ToListAsync();
        }
        
        protected async Task<List<Language>> GetSeededLanguages()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<Language>().ToListAsync();
        }
        
        protected async Task<List<StateProvince>> GetSeededStateProvinces()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<StateProvince>().ToListAsync();
        }
        
        protected async Task<List<Country>> GetSeededCountries()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<Country>().ToListAsync();
        }
        
        protected async Task<List<Company>> GetSeededCompanies()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<Company>().ToListAsync();
        }
        
        protected async Task<List<Position>> GetSeededPositions()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<Position>().ToListAsync();
        }
        
        protected async Task<List<Project>> GetSeededProjects()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<Project>().ToListAsync();
        }
        
        protected async Task<List<ProjectHighlight>> GetSeededProjectHighlights()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<ProjectHighlight>().ToListAsync();
        }
        
        protected async Task<List<Highlight>> GetSeededHighlights()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<Highlight>().ToListAsync();
        }
        
        protected async Task<List<CompanySkill>> GetSeededCompanySkills()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<CompanySkill>().ToListAsync();
        }
        
        protected async Task<List<Reference>> GetSeededReferences()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<Reference>().ToListAsync();
        }
        
        protected async Task<List<Resume>> GetSeededResumes()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<Resume>().ToListAsync();
        }
        
        protected async Task<List<ResumeSettings>> GetSeededResumeSettings()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<ResumeSettings>().ToListAsync();
        }
        
        protected async Task<List<ResumeCompany>> GetSeededResumeCompanies()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<ResumeCompany>().ToListAsync();
        }
        
        protected async Task<List<ResumeSkill>> GetSeededResumeSkills()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<ResumeSkill>().ToListAsync();
        }
        
        protected async Task<List<SkillCategory>> GetSeededSkillCategories()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<SkillCategory>().ToListAsync();
        }
        
        protected async Task<List<SkillCategorySkill>> GetSeededSkillCategorySkills()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<SkillCategorySkill>().ToListAsync();
        }
        
        protected async Task<List<OrganizationSettings>> GetSeededOrganizationSettings()
        {
            using var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            return await context.Set<OrganizationSettings>().ToListAsync();
        }
    }
}
