using connectify.application.common.models;
using connectify.domain.entities;
using connectify.domain.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace connectify.api.controllers;

[ApiController]
[Route("api/v1/tags")]
public class tagscontroller : ControllerBase
{
    private readonly iunitofwork _unitOfWork;

    public tagscontroller(iunitofwork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag([FromBody] string tagDescription)
    {
        var tag = new tag { tag_description = tagDescription };
        await _unitOfWork.repository<tag>().addasync(tag);
        await _unitOfWork.completeasync();

        return Ok(apiresponse<tag>.success(tag, "Tag created successfully"));
    }

    [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var tags = await _unitOfWork.repository<tag>().getallasync();
        return Ok(apiresponse<IEnumerable<tag>>.success(tags));
    }
}
