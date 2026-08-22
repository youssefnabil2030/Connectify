namespace connectify.application.dtos.posts;

public record postresponsedto(
    int id, 
    int user_id, 
    string? text_caption, 
    string visibility_setting, 
    datetime publication_date
);
