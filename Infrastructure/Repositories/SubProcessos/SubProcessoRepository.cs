using Microsoft.EntityFrameworkCore;
using StageTest.Domain.Entities;

namespace StageTest.Infrastructure.Repositories.SubProcessos
{
    public class SubProcessoRepository : Repository<Subprocesso>, ISubProcessoRepository
    {
        private readonly AppDbContext _context;
        public SubProcessoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteAllSubprocessos(int id)
        {
            // Desabilita as constraints de chave estrangeira
            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Subprocessos NOCHECK CONSTRAINT ALL");

            // Exclui os subprocessos
            var subprocessosParaExcluir = await _context.Subprocessos
                .Where(s => s.ProceId == id)
                .ToListAsync();

            _context.Subprocessos.RemoveRange(subprocessosParaExcluir);
            await _context.SaveChangesAsync();

            // Reabilita as constraints de chave estrangeira
            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Subprocessos CHECK CONSTRAINT ALL");

        }

        public async Task DeleteProcesso(int id)
        {

            Processo processo = await _context.Processos
            .Include(p => p.Ferramentas)
            .Include(p => p.Documentacoes)
            .FirstOrDefaultAsync(p => p.Id == id);

            if (processo == null)
            {
                throw new Exception("Processo não encontrado.");
            }

            // Remover as ferramentas e documentações associadas ao processo
            _context.ProcessoFerramentas.RemoveRange(processo.Ferramentas);
            _context.ProcessoDocumentacoes.RemoveRange(processo.Documentacoes);

            // Agora, você pode excluir o processo (sem afetar subprocessos ou responsáveis)
            _context.Processos.Remove(processo);

            // Salvar as alterações no banco
            await _context.SaveChangesAsync();
        }
    }
}
