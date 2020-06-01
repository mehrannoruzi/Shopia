using Elk.Core;

namespace Shopia.Domain
{
    public class ProductFilterDTO : PagingParameter
    {
        public int StoreId { get; set; }

        public ProductFilterCategory Category { get; set; }
    }
}
