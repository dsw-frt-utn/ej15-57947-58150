using Dsw2026EJ15.Api.DTOs;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026EJ15.Api.Controllers;
[ApiController]
[Route("api/[doctors]")]

public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateDoctorRequest request)
    {
        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality is null)
            throw new ValidationException("La especialidad no existe");
        
        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence.AddDoctor(doctor);

        return CreatedAtAction(nameof(GetById), new {id = doctor.Id}, null);
    }


    [HttpGet]
    public IActionResult GetAll()
    {
        var doctors = _persistence.GetActiveDoctors();
        var response = doctors.Select(d => new DoctorResponse
        {
            Name = d.Name,
            LicenseNumber = d.LicenseNumber,
            SpecialityName = d.Speciality.Name
        });
        return Ok(response);
    }

    public IActionResult GetById(Guid id)
    {
        var doctor = _persistence.GetActiveDoctorById(id);
        if (doctor is null)
            return NotFound();

        var response = new DoctorResponse
        {
            Name = doctor.Name,
            LicenseNumber = doctor.LicenseNumber,
            SpecialityName = doctor.Speciality.Name
        };
        return Ok(response);;
    }



    [HttpDelete("{id}")]
    public IActionResult Delete (Guid id)
    {
        var doctor = _persistence.GetActiveDoctorById(id);
        if (doctor is null)
            return NotFound();

        _persistence.DeactivateDoctor(id);
        return NoContent();
    }
}
