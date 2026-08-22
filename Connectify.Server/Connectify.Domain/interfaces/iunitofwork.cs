namespace connectify.domain.interfaces;

public interface iunitofwork : idisposable
{
    igenericrepository<t> repository<t>() where t : class;
    task<int> completeasync();
}
