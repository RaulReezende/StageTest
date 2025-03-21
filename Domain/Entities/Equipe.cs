using System.Text.Json.Serialization;

namespace StageTest.Domain.Entities;

public class Equipe
{
    public int Id { get; set; }
    public string Nome { get; set; }

    [JsonIgnore]
    public ICollection<Responsavel> Responsaveis { get; set; }

    [JsonIgnore]
    public ICollection<Processo> Processos { get; set; }
}
