using System.Text.Json.Serialization;

namespace HealthCare.Api.Models
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime DoB { get; set; }
        public int Age => (DateTime.Now - DoB).Days / 365;
    }
}
