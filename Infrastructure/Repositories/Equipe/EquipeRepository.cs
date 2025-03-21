using Microsoft.EntityFrameworkCore;
using StageTest.Application.DTOs;
using StageTest.Domain.Entities;

namespace StageTest.Infrastructure.Repositories;

public class EquipeRepository : Repository<Equipe>, IEquipeRepository
{
    private readonly AppDbContext _context;
    public EquipeRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ResponseGetDetailEquipe> GetDetailEquipes(int idEquipe)
    {
        var equip = await _context.Equipe.Include(x => x.Responsaveis).Where(x => x.Id == idEquipe).FirstOrDefaultAsync() ?? throw new Exception("Não foi encontrado equipe.");

        return new ResponseGetDetailEquipe()
        {
            Id = equip.Id,
            Nome = equip.Nome,
            Responsaveis = equip.Responsaveis.ToList()
        };
    }

    public async Task<Equipe> GetByIdWithResponsaveisAsync(int id)
    {
        return await _context.Equipe
            .Include(e => e.Responsaveis)
            .FirstOrDefaultAsync(e => e.Id == id);
    }


}
