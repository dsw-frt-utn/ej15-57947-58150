using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Api.Dtos;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }


    [HttpPost]
    public IActionResult AddDoctor(DoctorRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ValidationException("El nombre es requerido.");
        }

        if (string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            throw new ValidationException("La matrícula es requerida.");
        }

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality == null)
        {
            throw new ValidationException("La especialidad indicada no existe.");
        }

        var doctor = new Doctor
        {
            Name = request.Name,
            LicenseNumber = request.LicenseNumber,
            Speciality = speciality,
            IsActive = true
        };

        _persistence.AddDoctor(doctor);

        return StatusCode(201);
    }

    [HttpGet]
    public IActionResult GetActiveDoctors()
    {
        var doctors = _persistence.GetActiveDoctors();

        var response = doctors.Select(d => new DoctorResponseDto
        {
            Name = d.Name,
            LicenseNumber = d.LicenseNumber,
            SpecialityName = d.Speciality.Name
        }).ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetDoctorById(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);

        if (doctor == null || !doctor.IsActive)
        {
            return NotFound();
        }

        var response = new DoctorResponseDto
        {
            Name = doctor.Name,
            LicenseNumber = doctor.LicenseNumber,
            SpecialityName = doctor.Speciality.Name
        };

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public IActionResult DeactivateDoctor(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);

        if (doctor == null || !doctor.IsActive)
        {
            return NotFound();
        }

        doctor.IsActive = false;

        return NoContent();
    }
}