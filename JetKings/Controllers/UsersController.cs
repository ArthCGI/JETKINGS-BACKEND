using JetKings.IService;
using JetKings.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JetKings.Controllers;

public class UsersController : BaseApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
    {
        var result = await _userService.GetAllAsync(page, pageSize, ct);
        return OkResponse(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
    {
        var result = await _userService.GetByIdAsync(id, ct);
        return NotFoundResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequestDto dto, CancellationToken ct = default)
    {
        var result = await _userService.CreateAsync(dto, ct);
        return CreatedResponse(result, $"/api/users/{result.Data?.Id}");
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequestDto dto, CancellationToken ct = default)
    {
        var result = await _userService.UpdateAsync(id, dto, ct);
        return NotFoundResponse(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
    {
        var result = await _userService.DeleteAsync(id, ct);
        return NotFoundResponse(result);
    }
}
