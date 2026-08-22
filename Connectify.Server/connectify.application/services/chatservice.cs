using connectify.application.dtos.chat;
using connectify.application.interfaces;
using connectify.domain.entities;
using connectify.domain.interfaces;

namespace connectify.application.services;

public class chatservice : ichatservice
{
    private readonly iunitofwork _unitofwork;

    public chatservice(iunitofwork unitofwork)
    {
        _unitofwork = unitofwork;
    }

    public async task<messageresponsedto> sendmessageasync(sendmessagedto dto)
    {
        var message = new message
        {
            conversation_id = dto.conversation_id,
            user_id = dto.user_id,
            content = dto.content
        };

        await _unitofwork.repository<message>().addasync(message);
        await _unitofwork.completeasync();

        return new messageresponsedto(
            message.id, 
            message.conversation_id, 
            message.user_id, 
            message.content, 
            message.sent_time
        );
    }

    public async task<ienumerable<messageresponsedto>> getmessagesasync(int conversation_id)
    {
        var messages = await _unitofwork.repository<message>()
            .findasync(m => m.conversation_id == conversation_id);

        return messages.select(m => new messageresponsedto(
            m.id, m.conversation_id, m.user_id, m.content, m.sent_time
        ));
    }
}
