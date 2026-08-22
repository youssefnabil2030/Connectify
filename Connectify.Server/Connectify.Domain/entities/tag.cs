using connectify.domain.common;

namespace connectify.domain.entities;

public class tag : baseentity
{
    public string tag_description { get; set; } = string.empty;

    // navigation property
    public icollection<tagpost> tagposts { get; set; } = new list<tagpost>();
}
