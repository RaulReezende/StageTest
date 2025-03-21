namespace StageTest.Application.DTOs;

public class EditProcessDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int DepartamentoId { get; set; }
    public List<string> Ferramentas { get; set; }
    public int Responsaveis { get; set; }
    public List<DocumentoDto> Documentacoes { get; set; }
    public List<SubprocessoDto> Subprocessos { get; set; }



    public class DocumentoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string URL { get; set; }
    }

    public class SubprocessoDto
    {
        public string Nome { get; set; }
        public List<SubprocessoDto>? SubprocessosFilhos { get; set; }
    }


}
