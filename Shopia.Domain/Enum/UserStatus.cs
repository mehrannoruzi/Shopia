using System;

namespace Shopia.Domain
{
    public enum UserStatus : byte
    {
        MobileVerified = 1,
        AddStore = 2,
        AddAddress = 3,
        UploadDocument = 4,
        AddAcount = 5
    }
}
