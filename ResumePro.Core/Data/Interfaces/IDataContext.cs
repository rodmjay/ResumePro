#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ResumePro.Core.Data.Interfaces;

public interface IDataContext : IDisposable
{
    DatabaseFacade DatabaseFacade { get; }
    object GetKey<TEntity>(TEntity entity);
    int SaveChanges();
    void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
    void SyncObjectsStatePostCommit();
}