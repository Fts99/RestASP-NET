using System.Text.Json.Serialization;

namespace RestASPNET.Data.VO
{
    public class PersonVO
    {
        [JsonPropertyName("Codigo")]
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }
    }
}
