using System.Collections.Generic;

namespace Shopia.Domain
{
    public class AloPeikAddressInquiry
    {
        public List<string> Address { get; set; }
        public string Province { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string City_fa { get; set; }
    }

    public class AloPeikAddress : LocationDTO
    {
        public string Address { get; set; }
        public string Type { get; set; }
        public string City { get; set; }
        public string City_Fa { get; set; }
        public string Distance { get; set; }
        public string Duration { get; set; }
        public string Coefficient { get; set; }
        public string Price { get; set; }
        public string Priority { get; set; }
    }
}
