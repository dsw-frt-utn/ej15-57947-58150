using Dsw2026Ej15.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly AppDbContext _context;

        // Inyectamos el AppDbContext que creamos recién
        public PersistenceEf(AppDbContext context)
        {
            _context = context;
        }

        // 1. Obtener todos los médicos activos de la base de datos e incluir su especialidad
        public List<Doctor> GetDoctors()
        {
            return _context.Doctors
                .Include(d => d.Speciality)
                .Where(d => d.IsActive)
                .ToList();
        }

        // 2. Obtener todas las especialidades guardadas en la BD
        public List<Speciality> GetSpecialities()
        {
            return _context.Specialities.ToList();
        }

        // 3. Agregar un nuevo médico físicamente
        public Doctor AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return doctor;
        }

        // 4. Buscar un médico activo por ID incluyendo su especialidad
        public Doctor? GetDoctorById(Guid id)
        {
            return _context.Doctors
                .Include(d => d.Speciality)
                .FirstOrDefault(d => d.Id == id && d.IsActive);
        }

        // 5. Actualizar el estado de un médico en la BD (ej. el borrado lógico)
        public void UpdateDoctor(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
        }
    }
}