﻿using Microsoft.AspNetCore.Http;

namespace SourceSafe.Contracts.File;

public class Check_outRequest
{
    public int UserId { get; set; }
    public int FileId { get; set; }
    public IFormFile FormFile { get; set; } = null!;
    public bool Edited { get; set; }
}
