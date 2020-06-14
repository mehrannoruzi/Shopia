namespace Shopia.Domain
{
    public class ProductCategoryModel
    {
        public int ItemId { get; set; }
        public int ParentId { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; }
    }
}
