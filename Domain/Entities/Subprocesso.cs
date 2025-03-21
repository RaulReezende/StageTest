using System.Text.Json.Serialization;

namespace StageTest.Domain.Entities
{
    public class Subprocesso
    {
        public int Id { get; set; }
        public required string Nome { get; set; }

        public int ProceId { get; set; }
        public int? SubprocessoPaiId { get; set; }

        [JsonIgnore]
        public Subprocesso? SubprocessoPai { get; set; }
        public ICollection<Subprocesso>? SubprocessosFilhos { get; set; }
    }
}
