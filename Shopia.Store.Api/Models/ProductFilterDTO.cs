namespace Shopia.Store.Api
{
    public class ProductFilterDTO
    {
        public int StoreId { get; set; }

        public ProductCategory Category { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }

    public enum ProductCategory : byte
    {
        All = 0,
        Newests = 1,
        Favorites = 2,
        BestSellers = 3
    }
}
