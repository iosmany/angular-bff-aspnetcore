using System.Text.Json.Serialization;

namespace HealthCare.Api.Models
{
    [JsonSerializable(typeof(PatientDTO))]
    public class PatientDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
