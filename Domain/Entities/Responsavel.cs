using System.Text.Json.Serialization;

namespace StageTest.Domain.Entities
{
    public class Responsavel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }
        public int? EquipeId { get; set; } // Relacionamento: Um responsável pode estar relacionado a uma equipe

        // Relacionamento: Um responsável pode estar relacionado a vários processos ou subprocessos
        [JsonIgnore]
        public Equipe? Equipe { get; set; }

    }
}
