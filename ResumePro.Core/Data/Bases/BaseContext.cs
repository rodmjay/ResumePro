#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ResumePro.Core.Data.Helpers;
using ResumePro.Core.Data.Interfaces;

namespace ResumePro.Core.Data.Bases;

[ExcludeFromCodeCoverage]
public abstract class BaseContext<TContext> : DbContext, IDataContextAsync where TContext : DbContext
{
    protected BaseContext(DbContextOptions<TContext> options) : base(options)
    {
        if (_databaseInitialized) return;
        lock (_lock)
        {
            if (_databaseInitialized) return;
            InstanceId = Guid.NewGuid();
            _databaseInitialized = true;
        }
    }

    public Guid InstanceId { get; }

    public object GetKey<TEntity>(TEntity entity)
    {
        var entityType = Model.FindEntityType(entity.GetType());
        if (entityType == null) return default(int?);

        var entityKeys = entityType.FindPrimaryKey();
        var keyName = entityKeys.Properties.Select(x => x.Name).FirstOrDefault();
        return entity.GetType().GetProperty(keyName).GetValue(entity, null);
    }

    public override int SaveChanges()
    {
        var validationErrors =
            ChangeTracker.Entries<IValidatableObject>()
                .SelectMany(e => e.Entity.Validate(null))
                .Where(r => r != ValidationResult.Success)
                .ToList();

        if (validationErrors.Any())
        {
            var errors = validationErrors.ToDictionary(kvp => kvp.MemberNames, kvp => kvp.ErrorMessage)
                .Where(m => m.Value.Any());
            var err = string.Join(",", errors.Select(i => i.Value.ToString()).ToArray());
            throw new DbUpdateException(err, (Exception) null);
        }
        // Could also be before try if you know the exception occurs in SaveChanges

        SyncObjectsStatePreCommit();
        var changes = base.SaveChanges();
        SyncObjectsStatePostCommit();
        return changes;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await SaveChangesAsync(CancellationToken.None);
    }

    public Task<int> ExecuteSqlAsync(string query, object[] parameters)
    {
        throw new NotImplementedException();
        //return Database.ExecuteSqlRawAsync(query, parameters);
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        await SyncObjectsStatePreCommitAsync();
        var changesAsync = await base.SaveChangesAsync(cancellationToken);
        await SyncObjectsStatePostCommitAsync();
        return changesAsync;
    }

    public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
    {
        Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
    }

    public void SyncObjectsStatePostCommit()
    {
        var entries = ChangeTracker.Entries();
        foreach (var entityEntry in entries)
            if (entityEntry.Entity is IObjectState state)
                state.ObjectState = StateHelper.ConvertState(entityEntry.State);
    }

    public DatabaseFacade DatabaseFacade => Database;

    /// <summary>
    ///     Synchronizes the objects state post commit asynchronous.
    /// </summary>
    /// <returns></returns>
    public Task SyncObjectsStatePostCommitAsync()
    {
        foreach (var entityEntry in ChangeTracker.Entries())
            if (entityEntry.Entity is IObjectState state)
                state.ObjectState = StateHelper.ConvertState(entityEntry.State);
        return Task.CompletedTask;
    }

    protected abstract void SeedDatabase(ModelBuilder builder);

    protected abstract void ConfigureDatabase(ModelBuilder builder);

    protected sealed override void OnModelCreating(ModelBuilder builder)
    {
        ConfigureDatabase(builder);
        base.OnModelCreating(builder);
        SeedDatabase(builder);
    }


    private void SyncObjectsStatePreCommit()
    {
        var entries = ChangeTracker.Entries();
        foreach (var entityEntry in entries)
            if (entityEntry.Entity is IObjectState state)
            {
                entityEntry.State = StateHelper.ConvertState(state.ObjectState);

                switch (entityEntry.State)
                {
                    case EntityState.Added:
                    {
                        if (entityEntry.Entity is IHasCreationTime createdEntity)
                            createdEntity.Created = DateTime.UtcNow;

                        if (entityEntry is ICreated created) created.Created = DateTime.UtcNow;

                        break;
                    }
                    case EntityState.Modified:
                    {
                        if (entityEntry.Entity is IHasModificationTime modifiedEntity)
                            modifiedEntity.Updated = DateTime.UtcNow;

                        break;
                    }
                }
            }
    }

    private Task SyncObjectsStatePreCommitAsync()
    {
        SyncObjectsStatePostCommit();

        return Task.CompletedTask;
    }


    #region Private Fields

    private readonly bool _databaseInitialized;
    private readonly object _lock = new();

    #endregion Private Fields
}