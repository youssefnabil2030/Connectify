using System.Collections.Concurrent;
using connectify.domain.interfaces;
using connectify.infrastructure.persistence;

namespace connectify.infrastructure.persistence.repositories;

public class unitofwork : iunitofwork
{
    private readonly connectifydbcontext _context;
    private readonly concurrentdictionary<string, object> _repositories = new();

    public unitofwork(connectifydbcontext context)
    {
        _context = context;
    }

    public igenericrepository<t> repository<t>() where t : class
    {
        var type = typeof(t).name;
        return (igenericrepository<t>)_repositories.getoradd(type, _ => new genericrepository<t>(_context));
    }

    public async task<int> completeasync()
    {
        return await _context.savechangesasync();
    }

    public void dispose()
    {
        _context.dispose();
    }
}
