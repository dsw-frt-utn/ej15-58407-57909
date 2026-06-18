using System;
using System.Collections.Generic;
using System.Text;
namespace Dsw2026Ej15.Api.DTOs;

public class DoctorResponseDto
{
    public string Name { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string SpecialityName { get; set; } = string.Empty;
}