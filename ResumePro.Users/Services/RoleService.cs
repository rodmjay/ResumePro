#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel;
using Microsoft.AspNetCore.Identity;
using ResumePro.Core.Services.Bases;
using ResumePro.Users.Entities;
using ResumePro.Users.Interfaces;

namespace ResumePro.Users.Services;

public partial class RoleService : BaseService<Role>, IRoleService
{
    private readonly IdentityErrorDescriber _errors;

    private readonly IRepositoryAsync<RoleClaim> _roleClaimRepository;
    private bool _disposed;

    public RoleService(
        IdentityErrorDescriber _errors,
        IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _roleClaimRepository = UnitOfWork.RepositoryAsync<RoleClaim>();
        this._errors = _errors;
    }

    public bool AutoSaveChanges { get; set; } = true;
    public IQueryable<Role> Roles => Repository.Queryable();

    public void Dispose()
    {
        _disposed = true;
    }

    private Task SaveChanges(CancellationToken cancellationToken)
    {
        if (AutoSaveChanges)
        {
            var changes = Repository.Commit();
        }

        return Task.CompletedTask;
    }

    protected void ThrowIfDisposed()
    {
        if (_disposed) throw new ObjectDisposedException(GetType().Name);
    }

    public virtual string ConvertIdToString(int id)
    {
        if (id.Equals(default)) return null;
        return id.ToString();
    }

    public virtual int ConvertIdFromString(string id)
    {
        if (id == null) return default;
        return (int) TypeDescriptor.GetConverter(typeof(int)).ConvertFromInvariantString(id);
    }
}