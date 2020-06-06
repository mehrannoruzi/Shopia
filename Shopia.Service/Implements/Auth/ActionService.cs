using System;
using Elk.Core;
using System.Linq;
using Shopia.Domain;
using Shopia.DataAccess.Ef;
using System.Threading.Tasks;
using Shopia.Service.Resource;
using System.Linq.Expressions;
using System.Collections.Generic;
using Action = Shopia.Domain.Action;
using DomainStrings = Shopia.Domain.Resource.Strings;

namespace Shopia.Service
{
    public class ActionService : IActionService
    {
        private readonly AuthUnitOfWork _authUow;

        public ActionService(AuthUnitOfWork uow)
        {
            _authUow = uow;
        }


        public async Task<IResponse<Action>> AddAsync(Action model)
        {
            await _authUow.ActionRepo.AddAsync(model);

            var saveResult = _authUow.ElkSaveChangesAsync();
            return new Response<Action> { Result = model, IsSuccessful = saveResult.Result.IsSuccessful, Message = saveResult.Result.Message };
        }

        public async Task<IResponse<Action>> UpdateAsync(Action model)
        {
            var findedAction = await _authUow.ActionRepo.FindAsync(model.ActionId);
            if (findedAction == null) return new Response<Action> { Message = ServiceMessage.RecordNotExist.Fill(DomainStrings.Action) };

            findedAction.Name = model.Name;
            findedAction.Icon = model.Icon;
            findedAction.ParentId = model.ParentId;
            findedAction.ShowInMenu = model.ShowInMenu;
            findedAction.ActionName = model.ActionName;
            findedAction.OrderPriority = model.OrderPriority;

            var saveResult = _authUow.ElkSaveChangesAsync();
            return new Response<Action> { Result = findedAction, IsSuccessful = saveResult.Result.IsSuccessful, Message = saveResult.Result.Message };
        }

        public async Task<IResponse<bool>> DeleteAsync(int actionId)
        {
            _authUow.ActionRepo.Delete(new Action { ActionId = actionId });
            var saveResult = await _authUow.ElkSaveChangesAsync();
            return new Response<bool>
            {
                Message = saveResult.Message,
                Result = saveResult.IsSuccessful,
                IsSuccessful = saveResult.IsSuccessful,
            };
        }

        public async Task<IResponse<Action>> FindAsync(int actionId)
        {
            var findedAction = await _authUow.ActionRepo.FirstOrDefaultAsync(x => x.ActionId == actionId, new List<Expression<Func<Domain.Action, object>>> { i => i.Parent });
            if (findedAction == null) return new Response<Action> { Message = ServiceMessage.RecordNotExist.Fill(DomainStrings.Action) };
            return new Response<Action> { Result = findedAction, IsSuccessful = true };
        }

        public IDictionary<object, object> Get(bool justParents = false)
            => _authUow.ActionRepo.Get(x => !justParents || (x.ControllerName == null && x.ActionName == null),
                x => x.OrderByDescending(a => a.ActionId))
                .ToDictionary(k => (object)k.ActionId, v => (object)$"{v.Name}({v.ControllerName}/{v.ActionName})");

        public PagingListDetails<Action> Get(ActionSearchFilter filter)
        {
            Expression<Func<Action, bool>> conditions = x => true;
            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.NameF))
                    conditions = conditions.And(x => x.Name.Contains(filter.NameF));
                if (!string.IsNullOrWhiteSpace(filter.ActionNameF))
                    conditions = conditions.And(x => x.ActionName.Contains(filter.ActionNameF.ToLower()));
                if (!string.IsNullOrWhiteSpace(filter.ControllerNameF))
                    conditions = conditions.And(x => x.ControllerName.Contains(filter.ControllerNameF.ToLower()));
            }

            return _authUow.ActionRepo.Get(conditions, filter, x => x.OrderByDescending(u => u.ActionId), new List<Expression<Func<Domain.Action, object>>> {
                i=>i.Parent
            });
        }

        public IDictionary<object, object> Search(string searchParameter, int take = 10)
            => _authUow.ActionRepo.Get(conditions: x => x.Name.Contains(searchParameter) || x.ControllerName.Contains(searchParameter) || x.ActionName.Contains(searchParameter), o => o.OrderByDescending(x => x.ActionId))
            //.OrderByDescending(x => x.Name)
            .Take(take)
            .ToDictionary(k => (object)k.ActionId, v => (object)$"{v.Name}({(string.IsNullOrWhiteSpace(v.ControllerName) ? "" : v.ControllerName + "/" + v.ActionName)})");
    }
}

