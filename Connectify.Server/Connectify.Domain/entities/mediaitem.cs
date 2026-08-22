using connectify.domain.common;

namespace connectify.domain.entities;

public class mediaitem : baseentity
{
    public string media_type { get; set; } = string.empty; // "photo" | "video"
    public string file_url { get; set; } = string.empty;
    public string? resolution { get; set; }
    public int? deluration_ffor_videos { get; set; }
    public int? album_id { get; set; }
}
