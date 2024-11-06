﻿namespace SourceSafe.Infrastructure.Authentication;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string Secret { get; set; } = null!;
    public int ExpiryHours { get; set; }
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
}