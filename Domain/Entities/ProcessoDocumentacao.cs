namespace StageTest.Domain.Entities
{
    public class ProcessoDocumentacao
    {
        public int ProcessoDocumentacaoId { get; set; }
        public int? ProcessoId { get; set; }
        public int? SubprocessoId { get; set; }
        public int DocumentacaoId { get; set; }

        // Relacionamentos
        public Processo Processo { get; set; }
        public Subprocesso Subprocesso { get; set; }
        public Documentacao Documentacao { get; set; }
    }

}
