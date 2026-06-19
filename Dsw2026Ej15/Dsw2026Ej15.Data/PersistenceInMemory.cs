using System.Text.Json;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    private readonly List<Doctor> _doctors = new();
    private readonly List<Speciality> _specialities;

    public PersistenceInMemory()
    {
        _specialities = LoadSpecialities();
    }

    private List<Speciality> LoadSpecialities()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "specialities.json");
        var json = File.ReadAllText(path);
        var specialities = JsonSerializer.Deserialize<List<Speciality>>(json);
        return specialities ?? new List<Speciality>();
    }

    public void AddDoctor(Doctor doctor)
    {
        _doctors.Add(doctor);
    }

    public List<Doctor> GetActiveDoctors()
    {
        return _doctors.Where(d => d.IsActive).ToList();
    }

    public Doctor? GetDoctorById(Guid id)
    {
        return _doctors.Find(d => d.Id == id);
    }

    public Speciality? GetSpecialityById(Guid id)
    {
        return _specialities.Find(s => s.Id == id);
    }
}