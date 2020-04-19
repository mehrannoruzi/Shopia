using Shopia.Domain;
using System.Threading;
using Shopia.DataAccess.Ef;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public class NotificationService : INotificationService
    {
        readonly AppUnitOfWork _appUow;

        public NotificationService(AppUnitOfWork appUOW)
        {
            _appUow = appUOW;
        }


        public async Task<bool> AddAsync(Notification model, CancellationToken token = default)
        {
            await _appUow.NotificationRepo.AddAsync(model, token);
            var saveResult = await _appUow.ElkSaveChangesAsync(token);
            return saveResult.IsSuccessful;
        }


    }
}
