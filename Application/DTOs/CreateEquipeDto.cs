namespace StageTest.Application.DTOs;

public class CreateEquipeDto
{
    public string Nome { get; set; }
    public int Id { get; set; }
    public List<int> Responsaveis { get; set; }
}

public class CreateResponsavelDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
}
