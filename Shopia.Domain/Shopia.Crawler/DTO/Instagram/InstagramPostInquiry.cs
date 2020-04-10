using System.Collections.Generic;

namespace Shopia.Domain
{
    public class page_info
    {
        public bool has_next_page { get; set; }
        public string end_cursor { get; set; }
    }

    public class edge_owner_to_timeline_media
    {
        public int count { get; set; }
        public page_info page_Info { get; set; }
        public List<edges> edges { get; set; }
    }

    public class user
    {
        public edge_owner_to_timeline_media edge_Owner_To_Timeline_Media { get; set; }
    }

    public class data
    {
        public user user { get; set; }
    }

    public class InstagramPostInquiry
    {
        public string status { get; set; }
        public data data { get; set; }
    }
}
