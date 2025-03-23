using Bespoke.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumePro.Domain.Entities;
using ResumePro.Services.Interfaces;

namespace ResumePro.Services.Implementations;

public class IdGenerationService : IIdGenerationService
{
    private readonly IRepositoryAsync<Resume> _resumeRepo;
    private readonly ILogger<IdGenerationService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public IdGenerationService(
        IRepositoryAsync<Resume> resumeRepo,
        ILogger<IdGenerationService> logger,
        IServiceProvider serviceProvider)
    {
        _resumeRepo = resumeRepo;
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    private IQueryable<Resume> Resumes => _resumeRepo.Queryable();

    public async Task<int> GetNextResumeId(int organizationId)
    {
        // Simple implementation for testing
        var id = await Resumes.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
            
        return id + 1;
    }
}

