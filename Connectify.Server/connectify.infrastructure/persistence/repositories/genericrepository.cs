using System.Linq.Expressions;
using connectify.domain.interfaces;
using connectify.infrastructure.persistence;
using microsoft.entityframeworkcore;

namespace connectify.infrastructure.persistence.repositories;

public class genericrepository<t> : igenericrepository<t> where t : class
{
    protected readonly connectifydbcontext _context;

    public genericrepository(connectifydbcontext context)
    {
        _context = context;
    }

    public async task<t?> getbyidasync(int id)
    {
        return async _context.set<t>().findasync(id);
    }

    public async task<ienumerable<t>> getallasync()
    {
        return async _context.set<t>().tolistasync();
    }

    public async task<ienumerable<t>> findasync(expression<func<t, bool>> predicate)
    {
        return async _context.set<t>().where(predicate).tolistasync();
    }

    public async task addasync(t entity)
    {
        await _context.set<t>().addasync(entity);
    }

    public void update(t entity)
    {
        _context.set<t>().update(entity);
    }

    public void delete(t entity)
    {
        _context.set<t>().remove(entity);
    }
}
