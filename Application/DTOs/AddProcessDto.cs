namespace StageTest.Application.DTOs;

public class AddProcessDto
{
    public string Nome { get; set; }
    public int DepartamentoId { get; set; }
    public List<string> Ferramentas { get; set; }
    public int Responsaveis { get; set; }
    public List<AddSubprocessoDto> Subprocessos { get; set; }
    public List<AddDocumentoDto> Documentacoes { get; set; }
    

    
}
public class AddDocumentoDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string URL { get; set; }
}

public class AddSubprocessoDto
{
    public string Nome { get; set; }
    public List<AddSubprocessoDto>? SubprocessosFilhos { get; set; }
}