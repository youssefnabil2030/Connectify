namespace connectify.domain.common;

public abstract class auditableentity : baseentity
{
    public datetime created_at { get; set; } = datetime.utcnow;
    public datetime? updated_at { get; set; }
}
