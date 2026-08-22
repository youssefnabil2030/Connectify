namespace connectify.application.dtos.comments;

public record commentresponsedto(
    int id, 
    int user_id, 
    string comment_text, 
    string commentable_type, 
    int post_id, 
    datetime created_at
);
