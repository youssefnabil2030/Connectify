using connectify.domain.entities;
using microsoft.entityframeworkcore;
using microsoft.entityframeworkcore.metadata.builders;

namespace connectify.infrastructure.persistence.configurations;

public class commentconfiguration : ientitytypeconfiguration<comment>
{
    public void configure(entitytypebuilder<comment> builder)
    {
        builder.totable("comment_(poly)");
        builder.haskey(c => c.id);
        builder.property(c => c.id).hascolumnname("comment_id");

        builder.property(c => c.comment_text).isrequired();
        builder.property(c => c.commentable_type).hasmaxlength(20).isrequired();

        builder.hasone(c => c.user)
               .withmany(u => u.comments)
               .hasforeignkey(c => c.user_id)
               .ondelete(deletebehavior.restrict);
    }
}
