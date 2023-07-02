using System.Text.Json.Serialization;

namespace DataGenerator_Core.Entites
{
    public sealed class Template
    {
        public int ID { get; set; }
        public string? Data { get; set; }
        public int TypeID { get; set; }

        [JsonIgnore]
        public Type? Type { get; set; }

        [JsonPropertyName("type")]
        public string? TypeName { get { return Type?.Name; } }
    }
}
