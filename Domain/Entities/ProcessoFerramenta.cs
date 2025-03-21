namespace StageTest.Domain.Entities
{
    public class ProcessoFerramenta
    {
        public int ProcessoFerramentaId { get; set; }
        public int? ProcessoId { get; set; }
        public int? SubprocessoId { get; set; }
        public int FerramentaId { get; set; }

        // Relacionamentos
        public Processo Processo { get; set; }
        public Subprocesso Subprocesso { get; set; }
        public Ferramenta Ferramenta { get; set; }
    }
}
