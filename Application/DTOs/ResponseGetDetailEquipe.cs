using StageTest.Domain.Entities;

namespace StageTest.Application.DTOs;

public class ResponseGetDetailEquipe
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<Responsavel> Responsaveis { get; set; }
}
