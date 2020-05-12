using Shopia.Domain;
using System.Collections.Generic;

namespace Shopia.Store.Api
{
    public class AddOrderReponse
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public bool BasketChanged { get; set; }
        public IEnumerable<ProductDTO> ChangedProducts { get; set; }
}
}
