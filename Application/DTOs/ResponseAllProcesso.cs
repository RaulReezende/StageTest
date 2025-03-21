namespace StageTest.Application.DTOs;

public class ResponseAllProcesso()
{
    public int ProcessoId { get; set; }
    public string Nome { get; set; }
    public List<FerramentaDto> Ferramentas { get; set; }
    public ResponsavelDto Responsaveis { get; set; }
    public List<DocumentacaoDto> Documentacoes { get; set; }
    public List<SubprocessoDto> Subprocessos { get; set; }

}
public class FerramentaDto()
{
    public int FerramentaId { get; set; }
    public string Nome { get; set; }
}

public class ResponsavelDto()
{
    public int ResponsavelId { get; set; }
    public string Nome { get; set; }
}

public class DocumentacaoDto()
{
    public int DocumentacaoId { get; set; }
    public string Nome { get; set; }
}

public class SubprocessoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<SubprocessoDto>? Subprocessos { get; set; }
}