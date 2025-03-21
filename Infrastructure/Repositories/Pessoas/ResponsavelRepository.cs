using Microsoft.EntityFrameworkCore;
using StageTest.Domain.Entities;

namespace StageTest.Infrastructure.Repositories;

public class ResponsavelRepository : Repository<Responsavel>, IResponsavelRepository
{
    private readonly AppDbContext _context;
    public ResponsavelRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Responsavel>> GetResponsavelByEquipeId(int equipeId)
    {
        return await _context.Responsaveis.Where(r => r.EquipeId == equipeId).ToListAsync();
    }

    public async Task<List<Responsavel>> GetResponsaveis()
    {
        return await _context.Responsaveis.Where(r => r.EquipeId == null).ToListAsync();
    }

    public async Task<List<Responsavel>> GetByIdsAsync(IEnumerable<int> ids)
    {
        return await _context.Responsaveis
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();
    }
}
