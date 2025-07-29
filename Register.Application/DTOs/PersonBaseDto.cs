﻿namespace Register.Application.DTOs;

public class PersonBaseDto
{
    public string Name { get; set; } = string.Empty;
    public string? Gender { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string? PlaceOfBirth { get; set; }
    public string? Nationality { get; set; }
}
