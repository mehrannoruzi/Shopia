using System;
using System.ComponentModel;

namespace Shopia.Domain
{
    public enum NotificationType : byte
    {
        [Description("پیامک")]
        Sms = 1,

        [Description("تلگرام")]
        Telegram = 2,

        [Description("ایمیل")]
        Email = 3,
    }
}
