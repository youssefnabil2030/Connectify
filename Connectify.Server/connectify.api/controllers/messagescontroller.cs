using connectify.application.common.models;
using connectify.application.dtos.chat;
using connectify.application.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace connectify.api.controllers;

[ApiController]
[Route("api/v1/messages")]
public class messagescontroller : ControllerBase
{
    private readonly ichatservice _chatService;

    public messagescontroller(ichatservice chatService)
    {
        _chatService = chatService;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] sendmessagedto dto)
    {
        var result = await _chatService.sendmessageasync(dto);
        return Ok(apiresponse<messageresponsedto>.success(result, "Message sent successfully"));
    }

    [HttpGet("{conversationId:int}")]
    public async Task<IActionResult> GetMessages(int conversationId)
    {
        var result = await _chatService.getmessagesasync(conversationId);
        return Ok(apiresponse<IEnumerable<messageresponsedto>>.success(result));
    }
}
