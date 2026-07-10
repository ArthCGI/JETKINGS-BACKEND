using System.ComponentModel.DataAnnotations;

namespace JetKings.Models.DTOs;

public class UpdateUserRequestDto
{
    [Required, MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
