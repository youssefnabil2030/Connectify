using connectify.domain.common;

namespace connectify.domain.entities;

public class user : baseentity
{
    public string username { get; set; } = string.empty;
    public string email { get; set; } = string.empty;
    public string password { get; set; } = string.empty;
    public string? shortbio { get; set; }
    public string? profile_photo { get; set; }
    public string? cover_photo { get; set; }
    public datetime date_of_brith { get; set; }
    public datetime creation_date { get; set; } = datetime.utcnow;

    // navigation properties
    public setting? setting { get; set; }
    public icollection<post> posts { get; set; } = new list<post>();
    public icollection<comment> comments { get; set; } = new list<comment>();
    public icollection<message> messages { get; set; } = new list<message>();
    public icollection<notification> notifications { get; set; } = new list<notification>();
}
