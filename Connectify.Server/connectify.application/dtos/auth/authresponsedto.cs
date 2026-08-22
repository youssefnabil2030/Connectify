namespace connectify.application.dtos.auth;

public record authresponsedto(
    int user_id, 
    string username, 
    string email, 
    string token
);
