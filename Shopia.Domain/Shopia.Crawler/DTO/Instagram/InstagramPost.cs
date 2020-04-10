using System.Collections.Generic;

namespace Shopia.Domain
{
    public class dimensions
    {
        public int height { get; set; }
        public int width { get; set; }
    }

    public class resources
    {
        public string src { get; set; }
        public int config_width { get; set; }
        public int config_height { get; set; }
    }

    public class display_resources
    {
        public List<resources> resources { get; set; }
    }

    public class thumbnail_resources
    {
        public List<resources> resources { get; set; }
    }

    public class edge_media_preview_like
    {
        public int count { get; set; }
    }

    public class edge_media_to_comment
    {
        public int count { get; set; }
    }

    public class edge_media_to_caption
    {
        public List<edges> edges { get; set; }
    }

    public class edge_sidecar_to_children
    {
        public List<edges> edges { get; set; }
    }

    public class edges
    {
        public node node { get; set; }
    }

    public class node
    {
        public string text { get; set; }

        public string __typename { get; set; }
        public bool is_video { get; set; }
        public int video_view_count { get; set; }
        public string id { get; set; }
        public string shortcode { get; set; }
        public string video_url { get; set; }
        public string display_url { get; set; }
        public string taken_at_timestamp { get; set; }
        public string thumbnail_src { get; set; }
        public dimensions dimensions { get; set; }
        public List<resources> display_resources { get; set; }
        public List<resources> thumbnail_resources { get; set; }
        public edge_media_to_caption edge_Media_To_Caption { get; set; }
        public edge_media_to_comment edge_Media_To_Comment { get; set; }
        public edge_sidecar_to_children edge_sidecar_to_children { get; set; }
        public edge_media_preview_like edge_Media_Preview_Like { get; set; }
    }

    public class InstagramPost
    {
        public string __typename { get; set; }
        public string id { get; set; }
        public dimensions dimensions { get; set; }
        public string display_url { get; set; }
        public display_resources display_resources { get; set; }
        public bool is_video { get; set; }
        public string video_url { get; set; }
        public int video_view_count { get; set; }
        public string shortcode { get; set; }
        public string taken_at_timestamp { get; set; }
        public string thumbnail_src { get; set; }
        public edge_media_to_caption edge_Media_To_Caption { get; set; }
        public edge_media_to_comment edge_Media_To_Comment { get; set; }
        public edge_media_preview_like edge_Media_Preview_Like { get; set; }
        public thumbnail_resources thumbnail_resources { get; set; }
    }
}
