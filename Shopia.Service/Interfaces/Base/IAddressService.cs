using Elk.Core;
using Shopia.Domain;
using System;

namespace Shopia.Service
{
    public interface IAddressService
    {
        IResponse<PagingListDetails<AddressDTO>> Get(Guid userId);
    }
}