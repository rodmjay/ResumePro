//#region Header Info

//// Copyright 2024 Rod Johnson.  All rights reserved

//#endregion

//using System.Data;
//using Bespoke.Data.Enums;
//using Bespoke.Data.Interfaces;
//using Microsoft.EntityFrameworkCore.Storage;
//using ResumePro.Services.Interfaces;

//namespace ResumePro.Services.Implementations;

//public class IdGenerationService(
//    Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationContext> options,
//    IRepositoryAsync<Resume> resumeRepo,
//    IServiceProvider serviceProvider)
//    : BaseService(serviceProvider), IIdGenerationService
//{
//    private IQueryable<Resume> Resumes => resumeRepo.Queryable();

//    public async Task<int> GetNextResumeId(int organizationId)
//    {
//        int id = 0;

//        await using ApplicationContext context = new ApplicationContext(options);
//        IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();

//        await strategy.Execute(async () =>
//        {
//            await using IDbContextTransaction transaction =
//                await context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

//            Sequence sequence = await context.Set<Sequence>().FromSqlInterpolated(
//                    $"SELECT OrganizationId, ResumeId FROM [dbo].[Sequence] WITH (UPDLOCK, ROWLOCK) WHERE OrganizationId = {organizationId}")
//                .SingleOrDefaultAsync();

//            if (sequence == null)
//            {
//                sequence = new Sequence()
//                {
//                    ObjectState = ObjectState.Added,
//                    OrganizationId = organizationId,
//                    ResumeId = await GetNextResumeIdInternal(organizationId)
//                };

//                context.Set<Sequence>().Add(sequence);
//            }
//            else
//            {
//                sequence.ObjectState = ObjectState.Modified;
//            }

//            sequence.ResumeId++;

//            await context.SaveChangesAsync();
//            await transaction.CommitAsync();

//            id = sequence.ResumeId;
//        });

//        return id;
//    }

//    private async Task<int> GetNextResumeIdInternal(int organizationId)
//    {
//        var id = await Resumes.AsNoTracking()
//            .IgnoreQueryFilters()
//            .Where(x => x.OrganizationId == organizationId)
//            .OrderByDescending(x => x.Id)
//            .Select(x => x.Id)
//            .FirstOrDefaultAsync();
//        return id + 1;
//    }
//}