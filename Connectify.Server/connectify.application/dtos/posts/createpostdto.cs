namespace connectify.application.dtos.posts;

public record createpostdto(
    int user_id, 
    string? text_caption, 
    string visibility_setting
);
