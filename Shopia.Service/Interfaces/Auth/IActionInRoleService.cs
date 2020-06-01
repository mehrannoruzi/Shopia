using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Shopia.Service
{
    public interface IActionInRoleService : IScopedInjection
    {
        Task<IResponse<ActionInRole>> AddAsync(ActionInRole model);
        Task<IResponse<bool>> DeleteAsync(int id);
        IEnumerable<ActionInRole> GetRoles(int actionId);
        IEnumerable<ActionInRole> GetActions(int roleId);
    }
}