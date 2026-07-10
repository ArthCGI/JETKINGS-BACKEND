using JetKings.Application.DTOs.Request;
using JetKings.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace JetKings.API.Controllers;

public class UsersController : BaseApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>Gets a paginated list of users.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _userService.GetAllAsync(page, pageSize, cancellationToken);
        return OkResponse(result);
    }

    /// <summary>Gets a user by ID.</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _userService.GetByIdAsync(id, cancellationToken);
        return NotFoundResponse(result);
    }

    /// <summary>Creates a new user.</summary>
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserRequestDto dto,
        CancellationToken cancellationToken = default)
    {
        var result = await _userService.CreateAsync(dto, cancellationToken);
        return CreatedResponse(result, $"/api/users/{result.Data?.Id}");
    }

    /// <summary>Updates an existing user.</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateUserRequestDto dto,
        CancellationToken cancellationToken = default)
    {
        var result = await _userService.UpdateAsync(id, dto, cancellationToken);
        return NotFoundResponse(result);
    }

    /// <summary>Soft-deletes a user.</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _userService.DeleteAsync(id, cancellationToken);
        return NotFoundResponse(result);
    }
}
