using system.linq.expressions;

namespace connectify.domain.interfaces;

public interface igenericrepository<t> where t : class
{
    task<t?> getbyidasync(int id);
    task<ienumerable<t>> getallasync();
    task<ienumerable<t>> findasync(expression<func<t, bool>> predicate);
    task addasync(t entity);
    void update(t entity);
    void delete(t entity);
}
