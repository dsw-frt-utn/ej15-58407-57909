using System;
using System.Collections.Generic;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data;

public interface IPersistence
{
    void AddDoctor(Doctor doctor);
    IEnumerable<Doctor> GetActiveDoctors();
    Doctor? GetActiveDoctorById(Guid id);
    void DeactivateDoctor(Guid id);
    Speciality? GetSpecialityById(Guid id);
}
