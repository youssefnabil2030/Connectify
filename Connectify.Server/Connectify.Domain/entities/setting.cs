using connectify.domain.common;

namespace connectify.domain.entities;

public class setting : baseentity
{
    public int user_id { get; set; }
    public string privacy_preference { get; set; } = "public";
    public string notification_preference { get; set; } = "all";
    public string language_preference { get; set; } = "en";

    // navigation property
    public user user { get; set; } = null!;
}
