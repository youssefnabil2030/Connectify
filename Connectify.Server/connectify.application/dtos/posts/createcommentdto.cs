namespace connectify.application.dtos.comments;

public record createcommentdto(
    int user_id, 
    string comment_text, 
    string commentable_type, // "post" | "photo" | "video"
    int post_id             // target item id
);
