using System;
using Elk.Core;
using Shopia.Domain;
using System.Threading;
using System.Threading.Tasks;
using Elk.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Shopia.DataAccess.Ef
{
    public sealed class AppUnitOfWork : IElkUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly IServiceProvider _serviceProvider;

        public AppUnitOfWork(IServiceProvider serviceProvider, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _serviceProvider = serviceProvider;
        }


        #region Base
        public IUserRepo UserRepo => _serviceProvider.GetService<IUserRepo>();
        public ITagRepo TagRepo => _serviceProvider.GetService<ITagRepo>();
        public IGenericRepo<Address> AddressRepo => _serviceProvider.GetService<IGenericRepo<Address>>();
        public INotificationRepo NotificationRepo => _serviceProvider.GetService<INotificationRepo>();
        #endregion

        #region Order
        public IGenericRepo<Order> OrderRepo => _serviceProvider.GetService<IGenericRepo<Order>>();
        public IGenericRepo<OrderDetail> OrderDetailRepo => _serviceProvider.GetService<IGenericRepo<OrderDetail>>();
        #endregion

        #region Payment



        #endregion

        #region Store
        public IGenericRepo<Store> StoreRepo => _serviceProvider.GetService<IGenericRepo<Store>>();
        public IGenericRepo<Product> ProductRepo => _serviceProvider.GetService<IGenericRepo<Product>>();
        public IGenericRepo<ProductAsset> ProductAssetRepo => _serviceProvider.GetService<IGenericRepo<ProductAsset>>();
        public IGenericRepo<Discount> DiscountRepo => _serviceProvider.GetService<IGenericRepo<Discount>>();
        #endregion


        
        public ChangeTracker ChangeTracker { get => _appDbContext.ChangeTracker; }
        public DatabaseFacade Database { get => _appDbContext.Database; }

        public SaveChangeResult ElkSaveChanges()
            => _appDbContext.ElkSaveChanges();

        public Task<SaveChangeResult> ElkSaveChangesAsync(CancellationToken cancellationToken = default)
            => _appDbContext.ElkSaveChangesAsync(cancellationToken);

        public SaveChangeResult ElkSaveChangesWithValidation()
            => _appDbContext.ElkSaveChangesWithValidation();

        public Task<SaveChangeResult> ElkSaveChangesWithValidationAsync(CancellationToken cancellationToken = default)
            => _appDbContext.ElkSaveChangesWithValidationAsync(cancellationToken);

        public int SaveChanges()
            => _appDbContext.SaveChanges();

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _appDbContext.SaveChangesAsync(cancellationToken);

        public Task<int> SaveChangesAsync(bool AcceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
            => _appDbContext.SaveChangesAsync(AcceptAllChangesOnSuccess, cancellationToken);

        public void Dispose() => _appDbContext.Dispose();
    }
}