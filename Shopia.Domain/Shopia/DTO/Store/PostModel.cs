using System.Collections.Generic;
using System.Linq;

namespace Shopia.Domain
{
    public class PostModel
    {
        public bool HasImage { get { return !(Assets == null || !Assets.ToList().Any()); } }
        public string ImageUrl { get { return Assets?.ToList().FirstOrDefault()?.FileUrl; } }
        public string Description { get; set; }
        public string UniqueId { get; set; }
        public int Price { get; set; }
        public IEnumerable<PostAsset> Assets { get; set; }
    }
}
