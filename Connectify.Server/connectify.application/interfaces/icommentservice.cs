using connectify.application.dtos.comments;

namespace connectify.application.interfaces;

public interface icommentservice
{
    task<commentresponsedto> addcommentasync(createcommentdto dto);
    task<ienumerable<commentresponsedto>> getcommentsfortargetasync(string target_type, int target_id);
}
