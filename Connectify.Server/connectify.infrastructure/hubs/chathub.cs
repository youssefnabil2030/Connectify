using microsoft.aspnetcore.signalr;

namespace connectify.infrastructure.hubs;

public class chathub : hub
{
    public async task sendmessage(string user, string message)
    {
        await clients.all.sendasync("receivemessage", user, message);
    }

    public async task joinroom(string roomname)
    {
        await groups.addtoconnectionidasync(context.connectionid, roomname);
    }
}
