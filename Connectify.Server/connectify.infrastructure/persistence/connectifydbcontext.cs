using connectify.domain.entities;
using microsoft.entityframeworkcore;

namespace connectify.infrastructure.persistence;

public class connectifydbcontext : dbcontext
{
    public connectifydbcontext(dbcontextoptions<connectifydbcontext> options) : base(options) { }

    public dbset<user> users => set<user>();
    public dbset<setting> settings => set<setting>();
    public dbset<post> posts => set<post>();
    public dbset<comment> comments => set<comment>();
    public dbset<mediaitem> mediaitems => set<mediaitem>();
    public dbset<tag> tags => set<tag>();
    public dbset<tagpost> tagposts => set<tagpost>();
    public dbset<group> groups => set<group>();
    public dbset<message> messages => set<message>();
    public dbset<notification> notifications => set<notification>();

    protected override void onmodelcreating(modelbuilder modelbuilder)
    {
        base.onmodelcreating(modelbuilder);
        modelbuilder.applyconfigurationsfromassembly(typeof(connectifydbcontext).assembly);
    }
}
