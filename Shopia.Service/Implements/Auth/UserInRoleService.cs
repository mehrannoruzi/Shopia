using System;
using Elk.Core;
using System.Linq;
using Shopia.Domain;
using Shopia.DataAccess.Ef;
using System.Threading.Tasks;
using Shopia.Service.Resource;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Shopia.Service
{
    public class UserInRoleService : IUserInRoleService
    {
        private readonly AuthUnitOfWork _authUow;

        public UserInRoleService(AuthUnitOfWork uow)
        {
            _authUow = uow;
        }


        public async Task<IResponse<UserInRole>> Add(UserInRole model)
        {
            if (await _authUow.UserInRoleRepo.AnyAsync(x => x.UserId == model.UserId && x.RoleId == model.RoleId))
                return new Response<UserInRole> { Message = ServiceMessage.DuplicateRecord, IsSuccessful = false };

            await _authUow.UserInRoleRepo.AddAsync(model);
            var saveResult = await _authUow.ElkSaveChangesAsync();
            return new Response<UserInRole>
            {
                Result = model,
                Message = saveResult.Message,
                IsSuccessful = saveResult.IsSuccessful
            };
        }

        public async Task<IResponse<bool>> Delete(int userInRoleId)
        {
            _authUow.UserInRoleRepo.Delete(new UserInRole { UserInRoleId = userInRoleId });
            var saveResult = await _authUow.ElkSaveChangesAsync();
            return new Response<bool>
            {
                Message = saveResult.Message,
                Result = saveResult.IsSuccessful,
                IsSuccessful = saveResult.IsSuccessful,
            };
        }

        public IEnumerable<UserInRole> Get(Guid userId)
            => _authUow.UserInRoleRepo.Get(x => x.UserId == userId,
            x => x.OrderByDescending(uir => uir.UserId),
            new List<Expression<Func<UserInRole, object>>> { x => x.Role }).ToList();
    }
}
