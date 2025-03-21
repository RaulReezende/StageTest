using StageTest.Application.DTOs;
using StageTest.Domain.Entities;

namespace StageTest.Infrastructure.Repositories
{
    public interface IEquipeRepository : IRepository<Equipe>
    {
        Task<ResponseGetDetailEquipe> GetDetailEquipes(int id);
        Task<Equipe> GetByIdWithResponsaveisAsync(int id);
    }
}
