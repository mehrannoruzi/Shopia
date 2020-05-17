﻿using System.Collections.Generic;

namespace Shopia.Domain
{
    public class AloPeikPriceInquiryResult
    {
        public int Price { get; set; }
        public List<AloPeikAddress> Addresses { get; set; }
        public string Distance { get; set; }
        public string Duration { get; set; }
        public int Delay { get; set; }
        public bool Has_Return { get; set; }
        public bool Cashed { get; set; }
        public int Price_With_Return { get; set; }
        public int Final_Price { get; set; }
        public int Discount { get; set; }
    }
}
