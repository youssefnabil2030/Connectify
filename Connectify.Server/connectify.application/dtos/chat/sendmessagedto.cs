namespace connectify.application.dtos.chat;

public record sendmessagedto(
    int conversation_id, 
    int user_id, 
    string content
);
