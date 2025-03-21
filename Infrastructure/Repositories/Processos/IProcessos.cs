using StageTest.Application.DTOs;
using StageTest.Domain.Entities;

namespace StageTest.Infrastructure.Repositories;

public interface IProcessosRepository : IRepository<Processo>
{
    Task<List<ResponseAllProcesso>> GetAll(int DepartamentoId);
    Task UpdateProcessoComRelacionamentos(AddProcessDtoTest dto);
    Task<ResponseAllProcesso> GetProcesso(int departamentoId);
}
