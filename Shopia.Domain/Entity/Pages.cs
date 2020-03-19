using System;
using Shopia.Domain.Enum;

namespace Shopia.Domain.Entity
{
    public class Pages
    {
        public int PagesId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public PageStatus PageStatus { get; set; }
        public string CrawlSchedulePattern { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
