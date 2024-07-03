#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Data.Enums;

namespace ResumePro.Core.Data.Interfaces;

public interface IUnitOfWork : IDisposable
{
    event EventHandler OnSaveChanges;
    int SaveChanges();
    void Dispose(bool disposing);
    IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObjectState;
    void BeginTransaction(DbIsolationLevel isolationLevel = DbIsolationLevel.Unspecified);
    bool Commit();
    void Rollback();
}