using System.Text.Json.Serialization;

namespace StageTest.Domain.Entities
{
    public class Documentacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string URL { get; set; }

        // Relacionamento: Uma documentação pode estar relacionada a vários processos ou subprocessos
        [JsonIgnore]
        public ICollection<ProcessoDocumentacao> Processos { get; set; }
    }
}
