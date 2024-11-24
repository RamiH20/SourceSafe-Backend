﻿namespace SourceSafe.Application.Common.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}