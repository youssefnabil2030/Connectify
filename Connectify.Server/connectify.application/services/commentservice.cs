using connectify.application.dtos.comments;
using connectify.application.interfaces;
using connectify.application.common.exceptions;
using connectify.domain.entities;
using connectify.domain.interfaces;

namespace connectify.application.services;

public class commentservice : icommentservice
{
    private readonly iunitofwork _unitofwork;
    private static readonly string[] valid_types = { "post", "photo", "video" };

    public commentservice(iunitofwork unitofwork)
    {
        _unitofwork = unitofwork;
    }

    public async task<commentresponsedto> addcommentasync(createcommentdto dto)
    {
        var normalized_type = dto.commentable_type.tolowerinvariant();

        if (!valid_types.contains(normalized_type))
            throw new validationexception($"invalid commentable_type. allowed types: {string.join(", ", valid_types)}");

        var comment = new comment
        {
            user_id = dto.user_id,
            comment_text = dto.comment_text,
            commentable_type = normalized_type,
            post_id = dto.post_id
        };

        await _unitofwork.repository<comment>().addasync(comment);
        await _unitofwork.completeasync();

        return new commentresponsedto(
            comment.id, 
            comment.user_id, 
            comment.comment_text, 
            comment.commentable_type, 
            comment.post_id, 
            comment.created_at
        );
    }

    public async task<ienumerable<commentresponsedto>> getcommentsfortargetasync(string target_type, int target_id)
    {
        var comments = await _unitofwork.repository<comment>()
            .findasync(c => c.commentable_type == target_type.tolowerinvariant() && c.post_id == target_id);

        return comments.select(c => new commentresponsedto(
            c.id, c.user_id, c.comment_text, c.commentable_type, c.post_id, c.created_at
        ));
    }
}
