using Dsw2026Ej15.Domain;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Dsw2026Ej15.Data;

public interface IPersistence
{
    void AddDoctor(Doctor doctor);
    List<Doctor> GetActiveDoctors();
    Doctor? GetDoctorById(Guid id);
    Speciality ? GetSpecialityById (Guid id);




}
