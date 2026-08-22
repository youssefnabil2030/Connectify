namespace connectify.application.dtos.chat;

public record messageresponsedto(
    int id, 
    int conversation_id, 
    int user_id, 
    string content, 
    datetime sent_time
);
