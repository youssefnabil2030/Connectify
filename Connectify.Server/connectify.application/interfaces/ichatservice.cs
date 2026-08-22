using connectify.application.dtos.chat;

namespace connectify.application.interfaces;

public interface ichatservice
{
    task<messageresponsedto> sendmessageasync(sendmessagedto dto);
    task<ienumerable<messageresponsedto>> getmessagesasync(int conversation_id);
}
