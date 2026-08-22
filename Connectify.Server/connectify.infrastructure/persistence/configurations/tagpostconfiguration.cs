using connectify.domain.entities;
using microsoft.entityframeworkcore;
using microsoft.entityframeworkcore.metadata.builders;

namespace connectify.infrastructure.persistence.configurations;

public class tagpostconfiguration : ientitytypeconfiguration<tagpost>
{
    public void configure(entitytypebuilder<tagpost> builder)
    {
        builder.totable("tag_post_(poly)");
        builder.haskey(tp => new { tp.tag_id, tp.taggable_type, tp.taggable_id });

        builder.property(tp => tp.taggable_type).hasmaxlength(20).isrequired();

        builder.hasone(tp => tp.tag)
               .withmany(t => t.tagposts)
               .hasforeignkey(tp => tp.tag_id)
               .ondelete(deletebehavior.cascade);
    }
}
