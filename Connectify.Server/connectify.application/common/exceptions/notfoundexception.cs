namespace connectify.application.common.exceptions;

public class notfoundexception : exception
{
    public notfoundexception(string name, object key) 
        : base($"entity \"{name}\" ({key}) was not found.") { }
}
