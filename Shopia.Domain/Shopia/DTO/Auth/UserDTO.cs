using System;

namespace Shopia.Store.Api
{
    public class UserDTO
    {
        public Guid Token { get; set; }

        public string Fullname { get; set; }

        public long MobileNumber { get; set; }

        public string Description { get; set; }
    }
}
