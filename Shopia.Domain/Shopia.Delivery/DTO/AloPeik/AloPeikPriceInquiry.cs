using System.Collections.Generic;

namespace Shopia.Domain
{
    public class AloPeikPriceInquiry
    {
        public List<AloPeikAddress> Addresses { get; set; }
        public string Transport_Type { get; set; }
        public bool Has_Return { get; set; }
        public bool Cashed { get; set; }
    }
}
