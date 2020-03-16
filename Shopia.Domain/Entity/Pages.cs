using System;

namespace Shopia.Domain.Entity
{
    public class Pages
    {
        public int PagesId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public int Follower { get; set; }
        public int Following { get; set; }
        public DateTime LastUpdate { get; set; }


        public bool IsActive { get; set; }
    }
}
