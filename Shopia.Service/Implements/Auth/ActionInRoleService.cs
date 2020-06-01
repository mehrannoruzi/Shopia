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
    public class ActionInRoleService : IActionInRoleService
    {
        private readonly AuthUnitOfWork _authUow;

        public ActionInRoleService(AuthUnitOfWork uow)
        {
            _authUow = uow;
        }


        public async Task<IResponse<ActionInRole>> AddAsync(ActionInRole model)
        {
            if (await _authUow.ActionInRoleRepo.AnyAsync(x => x.RoleId == model.RoleId && x.ActionId == model.ActionId))
                return new Response<ActionInRole> { Message = ServiceMessage.DuplicateRecord, IsSuccessful = false };

            if (model.IsDefault)
            {
                var existActionInRole = await _authUow.ActionInRoleRepo.FirstOrDefaultAsync(conditions: x => x.RoleId == model.RoleId && x.IsDefault);
                if (existActionInRole != null)
                    existActionInRole.IsDefault = false;
            }

            await _authUow.ActionInRoleRepo.AddAsync(model);
            var saveResult = await _authUow.ElkSaveChangesAsync();
            return new Response<ActionInRole>
            {
                Result = model,
                Message = saveResult.Message,
                IsSuccessful = saveResult.IsSuccessful,
            };
        }

        public async Task<IResponse<bool>> DeleteAsync(int id)
        {
            _authUow.ActionInRoleRepo.Delete(new ActionInRole { ActionInRoleId = id });
            var saveResult = await _authUow.ElkSaveChangesAsync();
            return new Response<bool>
            {
                Message = saveResult.Message,
                Result = saveResult.IsSuccessful,
                IsSuccessful = saveResult.IsSuccessful,
            };
        }

        public IEnumerable<ActionInRole> GetRoles(int actionId) =>
                _authUow.ActionInRoleRepo.Get(x => x.ActionId == actionId,
                x => x.OrderByDescending(air => air.ActionId),
                new List<Expression<Func<ActionInRole, object>>> { x => x.Role }).ToList();

        public IEnumerable<ActionInRole> GetActions(int roleId) =>
                    _authUow.ActionInRoleRepo.Get(x => x.RoleId == roleId,
                    x => x.OrderByDescending(air => air.ActionId),
                    new List<Expression<Func<ActionInRole, object>>> { x => x.Action }).ToList();
    }
}
