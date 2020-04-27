using System;

namespace Shopia.Domain
{
    public class UpdateNotificationDto
    {
        public int NotificationId { get; set; }
        public NotificationStatus Status { get; set; }
        public string SendStatus { get; set; }
        public DateTime SendDateMi { get; set; }
    }
}
