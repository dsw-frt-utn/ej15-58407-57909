using System.Text.Json;
using Dsw2026Ej15.Domain;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public IEnumerable<Doctor> GetActiveDoctors()
    {
        return Doctors.Where(d => d.IsActive).ToList();
    }

    public Doctor? GetActiveDoctorById(Guid id)
    {
        return Doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
    }

    public void DeactivateDoctor(Guid id)
    {
        var doctor = Doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
        if (doctor != null)
        {
            doctor.IsActive = false;
        }
    }

    public Speciality? GetSpecialityById(Guid id)
    {
        return Specialities.FirstOrDefault(s => s.Id == id);
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