using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data;

public class PersistenceEf : IPersistence
{
    private readonly AppDbContext _context;

    public PersistenceEf(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Speciality> GetAllSpecialities()
        => _context.Specialities.AsNoTracking().ToList();

    public Speciality? GetSpecialityById(Guid id)
        => _context.Specialities.AsNoTracking().FirstOrDefault(s => s.Id == id);

    

    public void AddDoctor(Doctor doctor)
    {
        _context.Specialities.Attach(doctor.Speciality);
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
    }

    public IEnumerable<Doctor> GetAllDoctors()
        => _context.Doctors.Include(d => d.Speciality).AsNoTracking().ToList();

    public IEnumerable<Doctor> GetActiveDoctors()
        => _context.Doctors.Include(d => d.Speciality).Where(d => d.IsActive).AsNoTracking().ToList();

    public Doctor? GetDoctorById(Guid id)
        => _context.Doctors.Include(d => d.Speciality).AsNoTracking().FirstOrDefault(d => d.Id == id);

    public Doctor? GetActiveDoctorById(Guid id)
        => _context.Doctors.Include(d => d.Speciality).AsNoTracking().FirstOrDefault(d => d.Id == id && d.IsActive);



    public void UpdateDoctor(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
        _context.SaveChanges();
    }


    public void DeactivateDoctor(Guid id)
    {
        var doctor = _context.Doctors.FirstOrDefault(d => d.Id == id);
        if (doctor is not null)
        {
            doctor.IsActive = false;
            _context.SaveChanges();
        }
    }


    public void RemoveDoctor(Guid id)
    {
        var doctor = _context.Doctors.FirstOrDefault(d => d.Id == id);
        if (doctor is not null)
        {
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
        }
    }
}
