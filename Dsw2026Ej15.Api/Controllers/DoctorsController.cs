using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers
{
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
        public IActionResult Create([FromBody] CreateDoctorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ValidationException("El nombre es requerido.");
            }

            if (string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                throw new ValidationException("La matrícula es requerida.");
            }

            var speciality = _persistence
                .GetSpecialities()
                .FirstOrDefault(s => s.Id == request.SpecialityId);

            if (speciality is null)
            {
                throw new ValidationException("La especialidad no existe.");
            }

            var doctor = new Doctor
            {
                Name = request.Name,
                LicenseNumber = request.LicenseNumber,
                Speciality = speciality,
                IsActive = true
            };

            _persistence.AddDoctor(doctor);

            var response = new
            {
                doctor.Name,
                doctor.LicenseNumber,
                SpecialityName = doctor.Speciality.Name
            };

            return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, response);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var doctors = _persistence
                .GetDoctors()
                .Where(d => d.IsActive)
                .Select(d => new
                {
                    d.Name,
                    d.LicenseNumber,
                    SpecialityName = d.Speciality.Name
                })
                .ToList();

            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var doctor = _persistence
                .GetDoctors()
                .FirstOrDefault(d => d.Id == id && d.IsActive);

            if (doctor is null)
            {
                return NotFound();
            }

            var response = new
            {
                doctor.Name,
                doctor.LicenseNumber,
                SpecialityName = doctor.Speciality.Name
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var doctor = _persistence
                .GetDoctors()
                .FirstOrDefault(d => d.Id == id && d.IsActive);

            if (doctor is null)
            {
                return NotFound();
            }

            doctor.IsActive = false;

            _persistence.UpdateDoctor(doctor);

            return NoContent();
        }
    }
}
