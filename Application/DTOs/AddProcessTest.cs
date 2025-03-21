using System.ComponentModel.DataAnnotations;

namespace StageTest.Application.DTOs;
public class AddProcessDtoTest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do processo é obrigatório")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O ID do departamento é obrigatório")]
    public int DepartamentoId { get; set; }

    [Required(ErrorMessage = "Pelo menos uma ferramenta deve ser informada")]
    public List<string> Ferramentas { get; set; } = new(); // Alterado para List<string>

    [Required(ErrorMessage = "O responsável é obrigatório")]
    public int Responsaveis { get; set; }

    public List<AddTsSubprocessoDto> Subprocessos { get; set; } = new();
    public List<string> Documentacoes { get; set; } = new();
}

public class AddTsSubprocessoDto
{
    public string Id { get; set; }
    [Required(ErrorMessage = "O nome do subprocesso é obrigatório")]
    public string Nome { get; set; }

    // Use um DTO diferente para filhos (opcional)
    public List<AddTsSubprocessoDto> Subprocessos { get; set; } = new();
}



public class AddTsDocumentacaoDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do documento é obrigatório")]
    public string Nome { get; set; }


    [Required(ErrorMessage = "O nome do subprocesso é obrigatório")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "URL inválida")]
    public string Url { get; set; }
}