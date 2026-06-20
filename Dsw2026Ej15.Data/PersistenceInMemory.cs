using System.Text.Json;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> _doctors;
        private readonly List<Speciality> _specialities;

        public PersistenceInMemory()
        {
            _specialities = LoadSpecialities();
            _doctors = new List<Doctor>();
        }

        public List<Doctor> GetDoctors()
        {
            return _doctors;
        }

        public List<Speciality> GetSpecialities()
        {
            return _specialities;
        }

        public Doctor AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
            return doctor;
        }

        public Doctor? GetDoctorById(Guid id)
        {
            return _doctors.FirstOrDefault(d => d.Id == id);
        }

        public void UpdateDoctor(Doctor doctor)
        {
            var index = _doctors.FindIndex(d => d.Id == doctor.Id);

            if (index >= 0)
            {
                _doctors[index] = doctor;
            }
        }

        private List<Speciality> LoadSpecialities()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "specialities.json");

            if (!File.Exists(filePath))
            {
                return new List<Speciality>();
            }

            var json = File.ReadAllText(filePath);

            var specialities = JsonSerializer.Deserialize<List<Speciality>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return specialities ?? new List<Speciality>();
        }
    }
}
