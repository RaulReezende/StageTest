using StageTest.Domain.Entities;

namespace StageTest.Infrastructure.Repositories;

public interface IDepartamentoRepository : IRepository<Departamento>
{
    Task<List<Departamento>> GetAll(int areaId);
}
