using connectify.domain.common;

namespace connectify.domain.entities;

public class notification : baseentity
{
    public int user_id_recieptent { get; set; }
    public string type { get; set; } = string.empty;
    public datetime sent_time { get; set; } = datetime.utcnow;

    // navigation property
    public user user { get; set; } = null!;
}
