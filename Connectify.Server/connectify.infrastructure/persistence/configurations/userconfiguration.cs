using connectify.domain.entities;
using microsoft.entityframeworkcore;
using microsoft.entityframeworkcore.metadata.builders;

namespace connectify.infrastructure.persistence.configurations;

public class userconfiguration : ientitytypeconfiguration<user>
{
    public void configure(entitytypebuilder<user> builder)
    {
        builder.totable("user");
        builder.haskey(u => u.id);
        builder.property(u => u.id).hascolumnname("user_id");

        builder.property(u => u.username).hasmaxlength(50).isrequired();
        builder.hasindex(u => u.username).isunique();

        builder.property(u => u.email).hasmaxlength(100).isrequired();
        builder.hasindex(u => u.email).isunique();

        builder.property(u => u.password).isrequired();

        builder.hasone(u => u.setting)
               .withone(s => s.user)
               .hasforeignkey<setting>(s => s.user_id)
               .ondelete(deletebehavior.cascade);
    }
}
