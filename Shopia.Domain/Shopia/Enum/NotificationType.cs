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

        [Description("ایمیل")]
        SmsTelegram = 4,

        [Description("ایمیل")]
        SmsEmail = 5,

        [Description("ایمیل")]
        TelegramEmail = 6,

        [Description("ایمیل")]
        SmsTelegramEmail = 7
    }
}
