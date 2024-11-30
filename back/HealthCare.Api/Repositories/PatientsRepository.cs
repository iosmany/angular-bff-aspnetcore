using HealthCare.Api.Models;

namespace HealthCare.Api.Repositories
{
    public sealed class PatientsRepository
    {
        public Task<List<PatientDTO>> GetAllAsync()
        {
            var patients = new List<PatientDTO>
            {
                new PatientDTO
                {
                    Id = 1,
                    Name = "John Doe",
                    DoB = new DateTime(1980, 1, 1),
                },
                new PatientDTO
                {
                    Id = 2,
                    Name = "Jane Doe",
                    DoB = new DateTime(1985, 1, 1),
                },
            };
            return Task.FromResult(patients);
        }
    }
}
