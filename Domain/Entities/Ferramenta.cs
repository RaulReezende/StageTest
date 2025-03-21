using System.Text.Json.Serialization;

namespace StageTest.Domain.Entities
{
    public class Ferramenta
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        // Relacionamento: Uma ferramenta pode estar relacionada a vários processos ou subprocessos
        [JsonIgnore]
        public ICollection<ProcessoFerramenta> Processos { get; set; }
    }
}
