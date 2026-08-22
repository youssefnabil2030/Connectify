using connectify.application.common.models;
using connectify.application.dtos.auth;
using connectify.application.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace connectify.api.controllers;

[ApiController]
[Route("api/v1/auth")]
public class authcontroller : ControllerBase
{
    private readonly iauthservice _authService;

    public authcontroller(iauthservice authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] registerrequestdto dto)
    {
        var result = await _authService.registerasync(dto);
        return Ok(apiresponse<authresponsedto>.success(result, "User registered successfully"));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] loginrequestdto dto)
    {
        var result = await _authService.loginasync(dto);
        return Ok(apiresponse<authresponsedto>.success(result, "User logged in successfully"));
    }
}
