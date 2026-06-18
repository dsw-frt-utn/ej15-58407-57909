using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain;
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

    // POST api/doctors
    [HttpPost]
    public IActionResult CreateDoctor([FromBody] CreateDoctorDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ValidationException("El nombre es requerido.");

        if (string.IsNullOrWhiteSpace(dto.LicenseNumber))
            throw new ValidationException("El número de licencia es requerido.");

        var speciality = _persistence.Specialities.FirstOrDefault(s => s.Id == dto.SpecialityId);
        if (speciality == null)
            throw new ValidationException("La especialidad indicada no existe.");

        var newDoctor = new Doctor
        {
            Name = dto.Name,
            LicenseNumber = dto.LicenseNumber,
            Speciality = speciality,
            IsActive = true
        };

        _persistence.AddDoctor(newDoctor);

        return Created($"/api/doctors/{newDoctor.Id}", newDoctor);
    }

    // GET api/doctors
    [HttpGet]
    public IActionResult GetActiveDoctors()
    {
        var activeDoctors = _persistence.Doctors
            .Where(d => d.IsActive)
            .ToList();

        return Ok(activeDoctors);
    }

    // GET api/doctors/{id}
    [HttpGet("{id:guid}")]
    public IActionResult GetDoctorById(Guid id)
    {
        var doctor = _persistence.Doctors.FirstOrDefault(d => d.Id == id && d.IsActive);

        if (doctor == null)
            return NotFound(new { message = "Médico no encontrado o inactivo." });

        var response = new DoctorResponseDto
        {
            Name = doctor.Name,
            LicenseNumber = doctor.LicenseNumber,
            SpecialityName = doctor.Speciality.Name
        };

        return Ok(response);
    }

    // DELETE api/doctors/{id}
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteDoctor(Guid id)
    {
        var doctor = _persistence.Doctors.FirstOrDefault(d => d.Id == id && d.IsActive);

        if (doctor == null)
            return NotFound(new { message = "Médico no encontrado o inactivo." });

        doctor.IsActive = false;

        return NoContent();
    }
}