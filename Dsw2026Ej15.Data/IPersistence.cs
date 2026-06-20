using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data
{
    public interface IPersistence
    {
        List<Doctor> GetDoctors();
        List<Speciality> GetSpecialities();

        Doctor AddDoctor(Doctor doctor);
        Doctor? GetDoctorById(Guid id);
        void UpdateDoctor(Doctor doctor);
    }
}