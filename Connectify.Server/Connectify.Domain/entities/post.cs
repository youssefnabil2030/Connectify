using connectify.domain.common;

namespace connectify.domain.entities;

public class post : baseentity
{
    public int user_id { get; set; }
    public string? text_caption { get; set; }
    public string visibility_setting { get; set; } = "public";
    public datetime publication_date { get; set; } = datetime.utcnow;

    // navigation properties
    public user user { get; set; } = null!;
}
