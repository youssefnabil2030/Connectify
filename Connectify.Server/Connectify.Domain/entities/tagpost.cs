namespace connectify.domain.entities;

public class tagpost
{
    public int tag_id { get; set; }
    public string taggable_type { get; set; } = string.empty; // "post" | "photo" | "video"
    public int taggable_id { get; set; }

    // navigation property
    public tag tag { get; set; } = null!;
}
