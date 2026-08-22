using connectify.domain.common;

namespace connectify.domain.entities;

public class group : baseentity
{
    public string group_name { get; set; } = string.empty;
    public string? description { get; set; }
    public string privacy_setting { get; set; } = "public";
    public datetime creation_date { get; set; } = datetime.utcnow;
}
