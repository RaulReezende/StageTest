using Microsoft.EntityFrameworkCore;
using StageTest.Domain.Entities;

namespace StageTest.Infrastructure.Repositories;

public class DepartamentoRepository: Repository<Departamento>, IDepartamentoRepository
{
    private readonly AppDbContext _context;
    public DepartamentoRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<Departamento>> GetAll(int numer)
    {
        var departamentos = await _context.Departamentos
        .ToListAsync();

        return departamentos;
    }
}
