using connectify.domain.common;

namespace connectify.domain.entities;

public class message : baseentity
{
    public int conversation_id { get; set; }
    public int user_id { get; set; }
    public string content { get; set; } = string.empty;
    public datetime sent_time { get; set; } = datetime.utcnow;

    // navigation property
    public user user { get; set; } = null!;
}
