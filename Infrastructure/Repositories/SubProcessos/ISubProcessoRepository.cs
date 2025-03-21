using StageTest.Domain.Entities;

namespace StageTest.Infrastructure.Repositories.SubProcessos
{
    public interface ISubProcessoRepository : IRepository<Subprocesso>
    {
        Task DeleteAllSubprocessos(int id);
        Task DeleteProcesso(int id);
    }
}
