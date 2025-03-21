using System.Text.Json.Serialization;

namespace StageTest.Domain.Entities;

public class Processo
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int DepartamentoId { get; set; }

    // Relacionamento: Um processo pertence a uma área
    public Departamento Departamento { get; set; }

    // Relacionamento: Um processo tem muitos subprocessos
    public ICollection<Subprocesso> Subprocessos { get; set; }

    // Relacionamentos adicionais
    [JsonIgnore]
    public ICollection<ProcessoFerramenta> Ferramentas { get; set; }
    [JsonIgnore]
    public Equipe Responsaveis { get; set; }
    [JsonIgnore]
    public ICollection<ProcessoDocumentacao> Documentacoes { get; set; }
}
