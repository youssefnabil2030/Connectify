using microsoft.aspnetcore.signalr;

namespace connectify.infrastructure.hubs;

public class notificationhub : hub
{
    public async task sendnotification(string userid, string message)
    {
        await clients.user(userid).sendasync("receivenotification", message);
    }
}
