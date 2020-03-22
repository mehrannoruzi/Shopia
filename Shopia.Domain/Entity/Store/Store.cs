using System;
using Elk.Core;
using Shopia.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopia.Domain
{
    [Table(nameof(Store), Schema = "Store")]
    public class Store : IInsertDateProperties, IModifyDateProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreId { get; set; }

        public Guid UserId { get; set; }
        public StoreStatus StoreStatus { get; set; }
        public DateTime InsertDateMi { get; set; }
        public DateTime ModifyDateMi { get; set; }
        public string InsertDateSh { get; set; }
        public string ModifyDateSh { get; set; }
        public string Name { get; set; }
        public string CrawlSchedulePattern { get; set; }
    }
}
