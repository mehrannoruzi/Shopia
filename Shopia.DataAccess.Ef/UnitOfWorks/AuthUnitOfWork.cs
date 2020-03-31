using System;
using Elk.Core;
using Shopia.Domain;
using System.Threading;
using System.Threading.Tasks;
using Elk.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Shopia.DataAccess.Ef
{
    public sealed class AuthUnitOfWork : IElkUnitOfWork
    {
        private Dictionary<Type, object> _repositories;
        private readonly AuthDbContext _authDbContext;
        private readonly IServiceProvider _serviceProvider;

        public AuthUnitOfWork(IServiceProvider serviceProvider, AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
            _serviceProvider = serviceProvider;
        }


        public IGenericRepo<Role> RoleRepo => _serviceProvider.GetService<IGenericRepo<Role>>();
        public IGenericRepo<Domain.Action> ActionRepo => _serviceProvider.GetService<IGenericRepo<Domain.Action>>();
        public IGenericRepo<ActionInRole> ActionInRoleRepo => _serviceProvider.GetService<IGenericRepo<ActionInRole>>();
        public IGenericRepo<UserInRole> UserInRoleRepo => _serviceProvider.GetService<IGenericRepo<UserInRole>>();


        public IGenericRepo<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                _repositories.Add(type, _serviceProvider.GetService<IGenericRepo<T>>());
            }

            return (IGenericRepo<T>)_repositories[type];
        }

        public ChangeTracker ChangeTracker { get => _authDbContext.ChangeTracker; }
        public DatabaseFacade Database { get => _authDbContext.Database; }

        public SaveChangeResult ElkSaveChanges()
            => _authDbContext.ElkSaveChanges();

        public Task<SaveChangeResult> ElkSaveChangesAsync(CancellationToken cancellationToken = default)
            => _authDbContext.ElkSaveChangesAsync(cancellationToken);

        public SaveChangeResult ElkSaveChangesWithValidation()
            => _authDbContext.ElkSaveChangesWithValidation();

        public Task<SaveChangeResult> ElkSaveChangesWithValidationAsync(CancellationToken cancellationToken = default)
            => _authDbContext.ElkSaveChangesWithValidationAsync(cancellationToken);

        public int SaveChanges()
            => _authDbContext.SaveChanges();

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _authDbContext.SaveChangesAsync(cancellationToken);

        public Task<int> SaveChangesAsync(bool AcceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
            => _authDbContext.SaveChangesAsync(AcceptAllChangesOnSuccess, cancellationToken);

        public void Dispose() => _authDbContext.Dispose();
    }
}
