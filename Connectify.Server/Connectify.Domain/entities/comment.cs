using connectify.domain.common;

namespace connectify.domain.entities;

public class comment : baseentity
{
    public int user_id { get; set; }
    public string comment_text { get; set; } = string.empty;
    public datetime created_at { get; set; } = datetime.utcnow;

    // polymorphic fields
    public string commentable_type { get; set; } = string.empty; // "post" | "photo" | "video"
    public int post_id { get; set; } // polymorphic target id

    // navigation properties
    public user user { get; set; } = null!;
}
