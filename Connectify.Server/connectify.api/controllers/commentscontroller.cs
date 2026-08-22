using connectify.application.common.models;
using connectify.application.dtos.comments;
using connectify.application.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace connectify.api.controllers;

[ApiController]
[Route("api/v1/comments")]
public class commentscontroller : ControllerBase
{
    private readonly icommentservice _commentService;

    public commentscontroller(icommentservice commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<IActionResult> AddComment([FromBody] createcommentdto dto)
    {
        var result = await _commentService.addcommentasync(dto);
        return Ok(apiresponse<commentresponsedto>.success(result, "Comment added successfully"));
    }

    [HttpGet("{targetType}/{targetId:int}")]
    public async Task<IActionResult> GetCommentsForTarget(string targetType, int targetId)
    {
        var result = await _commentService.getcommentsfortargetasync(targetType, targetId);
        return Ok(apiresponse<IEnumerable<commentresponsedto>>.success(result));
    }
}
