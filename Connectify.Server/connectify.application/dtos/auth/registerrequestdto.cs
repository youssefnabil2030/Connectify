namespace connectify.application.dtos.auth;

public record registerrequestdto(
    string username, 
    string email, 
    string password, 
    datetime date_of_brith
);
