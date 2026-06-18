using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Api.DTOs; 

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

   
    [HttpPost]
    public IActionResult CreateDoctor([FromBody] CreateDoctorDto request)
    {
       
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("El campo Name es requerido.");

        if (string.IsNullOrWhiteSpace(request.LicenseNumber))
            throw new ValidationException("El campo License Number es requerido.");

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality == null)
            throw new ValidationException("La especialidad indicada no existe.");

        var newDoctor = new Doctor
        {
            Name = request.Name,
            LicenseNumber = request.LicenseNumber,
            IsActive = true,
            Speciality = speciality
        };

        _persistence.AddDoctor(newDoctor);

        return StatusCode(201, new { Message = "Médico creado exitosamente", Id = newDoctor.Id });
    }

   
    [HttpGet]
    public IActionResult GetActiveDoctors()
    {
        var doctors = _persistence.GetActiveDoctors().Select(d => new DoctorResponseDto
        {
            Id = d.Id,
            Name = d.Name,
            LicenseNumber = d.LicenseNumber,
            SpecialityName = d.Speciality.Name 
        }).ToList();

        return Ok(doctors);
    }

   
    [HttpGet("{id:guid}")]
    public IActionResult GetDoctorById(Guid id)
    {
        var doctor = _persistence.GetActiveDoctorById(id);
        if (doctor == null)
            return NotFound(new { Error = "Médico no encontrado o se encuentra inactivo." });

        var response = new DoctorResponseDto
        {
            Id = doctor.Id,
            Name = doctor.Name,
            LicenseNumber = doctor.LicenseNumber,
            SpecialityName = doctor.Speciality.Name
        };

        return Ok(response);
    }

   
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteDoctor(Guid id)
    {
        var doctor = _persistence.GetActiveDoctorById(id);
        if (doctor == null)
            return NotFound(new { Error = "Médico no encontrado o ya se encuentra inactivo." });

        _persistence.DeactivateDoctor(id);

      
        return NoContent();
    }
}