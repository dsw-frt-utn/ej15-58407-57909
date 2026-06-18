using System.Text.Json;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    public List<Doctor> Doctors { get; private set; } = new();
    public List<Speciality> Specialities { get; private set; } = new();

    public PersistenceInMemory()
    {
        LoadSpecialities();
    }

    public void AddDoctor(Doctor doctor)
    {
        Doctors.Add(doctor);
    }

    private void LoadSpecialities()
    {
        if (File.Exists("specialities.json"))
        {
            var json = File.ReadAllText("specialities.json");
            Specialities = JsonSerializer.Deserialize<List<Speciality>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Speciality>();
        }
    }
}