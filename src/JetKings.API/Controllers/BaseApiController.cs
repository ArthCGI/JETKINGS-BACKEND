using JetKings.Application.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace JetKings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public abstract class BaseApiController : ControllerBase
{
    protected IActionResult OkResponse<T>(ApiResponseDto<T> response) =>
        response.Success ? Ok(response) : BadRequest(response);

    protected IActionResult CreatedResponse<T>(ApiResponseDto<T> response, string? location = null) =>
        response.Success ? Created(location ?? string.Empty, response) : BadRequest(response);

    protected IActionResult NotFoundResponse<T>(ApiResponseDto<T> response) =>
        response.Success ? Ok(response) : NotFound(response);
}
