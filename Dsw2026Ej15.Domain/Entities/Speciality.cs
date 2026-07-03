using Dsw2026Ej15.Domain.Exceptions;

namespace Dsw2026Ej15.Domain.Entities;

public class Speciality : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Speciality() { }
    public Speciality (string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("La especialidad necesita un nombre.");
        if (string.IsNullOrWhiteSpace(description))
            throw new ValidationException("La especialidad necesita una descripcion");

        Name = name;
        Description = description;
    }
}