using Dsw2026Ej15.Domain.Exceptions;

namespace Dsw2026Ej15.Domain.Entities;

public class Doctor : BaseEntity {
    public string Name { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public Speciality Speciality { get; set; } = null!;

    public Doctor() { }

    public Doctor(string name, string licenseNumber, Speciality speciality)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("El nombre del doctor no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(licenseNumber))
            throw new ValidationException("La matrícula del doctor no puede estar vacía.");
        Name = name;
        LicenseNumber = licenseNumber;
        Speciality = speciality;
        IsActive = true;
    }
}