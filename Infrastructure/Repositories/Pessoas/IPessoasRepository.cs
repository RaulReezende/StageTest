using StageTest.Domain.Entities;

namespace StageTest.Infrastructure.Repositories;

public interface IResponsavelRepository : IRepository<Responsavel>
{
    Task<List<Responsavel>> GetResponsavelByEquipeId(int equipeId);
    Task<List<Responsavel>> GetResponsaveis();
    Task<List<Responsavel>> GetByIdsAsync(IEnumerable<int> ids);
}
