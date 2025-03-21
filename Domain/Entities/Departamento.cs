using System.Text.Json.Serialization;

namespace StageTest.Domain.Entities;
public class Departamento
{
    public int Id { get; set; }
    public string Nome { get; set; }

    // Relacionamento: Uma área tem muitos processos
    [JsonIgnore]
    public ICollection<Processo> Processos { get; set; }
}
