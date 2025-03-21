namespace StageTest.Domain.Entities;
public class ProcessoResponsavel
{
    public int ProcessoResponsavelId { get; set; }
    public int? ProcessoId { get; set; }
    public int? SubprocessoId { get; set; }
    public int ResponsavelId { get; set; }

    // Relacionamentos
    public Processo Processo { get; set; }
    public Subprocesso Subprocesso { get; set; }
    public Responsavel Responsavel { get; set; }
}
