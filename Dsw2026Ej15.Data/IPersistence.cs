using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data;

public interface IPersistence
{
    List<Doctor> Doctors { get; }
    List<Speciality> Specialities { get; }
    void AddDoctor(Doctor doctor);
}
