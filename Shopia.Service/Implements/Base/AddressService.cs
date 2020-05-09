using System;
using Elk.Core;
using System.Linq;
using Shopia.Domain;
using Shopia.DataAccess.Ef;
using System.Threading.Tasks;

namespace Shopia.Service
{
    public class AddressService : IAddressService
    {
        readonly AppUnitOfWork _appUow;
        readonly IAddressRepo _addressRepo;
        public AddressService(AppUnitOfWork appUOW)
        {
            _appUow = appUOW;
            _addressRepo = appUOW.AddressRepo;
        }

        public IResponse<PagingListDetails<AddressDTO>> Get(Guid userId)
        {
            var currentDT = DateTime.Now;
            var addresses = _addressRepo.Get(selector: a => new AddressDTO
            {
                Id = a.AddressId,
                Lat = a.Latitude,
                Lng = a.Longitude,
                Address = a.AddressDetails
            },
            conditions: x => x.UserId == userId,
            pagingParameter: new PagingParameter
            {
                PageNumber = 1,
                PageSize = 3
            },
            orderBy: o => o.OrderByDescending(x => x.AddressId));
            return new Response<PagingListDetails<AddressDTO>>
            {
                Result = addresses,
                IsSuccessful = true
            };
        }
    }
}
