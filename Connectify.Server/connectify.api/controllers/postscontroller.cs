using connectify.application.common.models;
using connectify.application.dtos.posts;
using connectify.domain.entities;
using connectify.domain.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace connectify.api.controllers;

[ApiController]
[Route("api/v1/posts")]
public class postscontroller : ControllerBase
{
    private readonly iunitofwork _unitOfWork;

    public postscontroller(iunitofwork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] createpostdto dto)
    {
        var post = new post
        {
            user_id = dto.user_id,
            text_caption = dto.text_caption,
            visibility_setting = dto.visibility_setting
        };

        await _unitOfWork.repository<post>().addasync(post);
        await _unitOfWork.completeasync();

        var response = new postresponsedto(post.id, post.user_id, post.text_caption, post.visibility_setting, post.publication_date);
        return Ok(apiresponse<postresponsedto>.success(response, "Post created successfully"));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _unitOfWork.repository<post>().getallasync();
        var response = posts.Select(p => new postresponsedto(p.id, p.user_id, p.text_caption, p.visibility_setting, p.publication_date));
        return Ok(apiresponse<IEnumerable<postresponsedto>>.success(response));
    }
}
