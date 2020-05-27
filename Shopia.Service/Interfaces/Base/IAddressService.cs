using System;
using Elk.Core;
using Shopia.Domain;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public interface IAddressService
    {
        IResponse<PagingListDetails<AddressDTO>> Get(Guid userId);
        Task<IResponse<Address>> FindAsync(int id);
    }
}