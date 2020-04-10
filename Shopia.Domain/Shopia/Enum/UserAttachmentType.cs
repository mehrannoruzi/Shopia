using System;
using System.ComponentModel;

namespace Shopia.Domain
{
    public enum UserAttachmentType
    {
        [Description("کارت ملی(پشت و رو )")]
        NationalCard = 1,

        [Description("شناسنامه/پاسپورت/گواهینامه")]
        IdentityCard = 2,

        [Description("عکس پرسنلی")]
        PersoneliPicture = 3,
    }
}
