using Microsoft.EntityFrameworkCore;
using StageTest.Application.DTOs;
using StageTest.Domain.Entities;

namespace StageTest.Infrastructure.Repositories;

public class ProcessosRepository : Repository<Processo>, IProcessosRepository
{
    private readonly AppDbContext _context;
    public ProcessosRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<ResponseAllProcesso>> GetAll(int departamentoId)
    {
        List<Processo> processos = await _context.Processos
            .Include(p => p.Ferramentas)
                .ThenInclude(f => f.Ferramenta)
            .Include(p => p.Documentacoes)
                .ThenInclude(f => f.Documentacao)
            .Include(p => p.Subprocessos)
                .ThenInclude(p => p.SubprocessosFilhos)
            .Include(p => p.Responsaveis)
            .Where(d => d.DepartamentoId == departamentoId)
            .ToListAsync();

        var processosList = new List<ResponseAllProcesso>();

        foreach (var processo in processos)
        {
            processosList.Add(new ResponseAllProcesso
            {
                ProcessoId = processo.Id,
                Nome = processo.Nome,
                Ferramentas = processo.Ferramentas.Select(f => new FerramentaDto
                {
                    FerramentaId = f.Ferramenta.Id,
                    Nome = f.Ferramenta.Nome
                }).ToList(),
                Responsaveis = new ResponsavelDto
                {
                    ResponsavelId = processo.Responsaveis.Id,
                    Nome = processo.Responsaveis.Nome,
                },
                Documentacoes = processo.Documentacoes.Select(doc => new DocumentacaoDto
                {
                    DocumentacaoId = doc.Documentacao.Id,
                    Nome = doc.Documentacao.Nome,
                }).ToList(),
                Subprocessos = await GetSubprocessosRecursivos(processo.Subprocessos.ToList())
            });
        } 
            
        return processosList;
    }
    public async Task<ResponseAllProcesso> GetProcesso(int processoId)
    {
        Processo processo = await _context.Processos
            .Include(p => p.Ferramentas)
                .ThenInclude(f => f.Ferramenta)
            .Include(p => p.Documentacoes)
                .ThenInclude(f => f.Documentacao)
            .Include(p => p.Subprocessos)
                .ThenInclude(p => p.SubprocessosFilhos)
            .Include(p => p.Responsaveis)
            .Where(d => d.Id == processoId)
            .FirstOrDefaultAsync();

        if(processo == null)
        {
            throw new Exception("Processo não encontrado.");
        }

        var processosReponse = new ResponseAllProcesso()
        {
            ProcessoId = processo.Id,
            Nome = processo.Nome,
            Ferramentas = processo.Ferramentas.Select(f => new FerramentaDto
            {
                FerramentaId = f.Ferramenta.Id,
                Nome = f.Ferramenta.Nome
            }).ToList(),
            Responsaveis = new ResponsavelDto
            {
                ResponsavelId = processo.Responsaveis.Id,
                Nome = processo.Responsaveis.Nome,
            },
            Documentacoes = processo.Documentacoes.Select(doc => new DocumentacaoDto
            {
                DocumentacaoId = doc.Documentacao.Id,
                Nome = doc.Documentacao.Nome,
            }).ToList(),
            Subprocessos = await GetSubprocessosRecursivos(processo.Subprocessos.ToList())
        };

        return processosReponse;
    }
    private async Task<List<SubprocessoDto>> GetSubprocessosRecursivos(List<Subprocesso>? subprocessos)
    {
        if (subprocessos == null || !subprocessos.Any())
            return [];



        var resultado = new List<SubprocessoDto>();

        foreach (var s in subprocessos)
        {
            // Carrega os filhos do subprocesso atual
            var subprocessosFilhos = await _context.Subprocessos
                .Include(sp => sp.SubprocessosFilhos)
                .Where(sp => sp.SubprocessoPaiId == s.Id)
                .ToListAsync();

            // Adiciona o subprocesso ao resultado
            resultado.Add(new SubprocessoDto
            {
                Id = s.Id,
                Nome = s.Nome,
                Subprocessos = subprocessosFilhos != null ? await GetSubprocessosRecursivos(subprocessosFilhos) : []
            });
        }

        return resultado;
    }

    public async Task UpdateProcessoComRelacionamentos(AddProcessDtoTest dto)
    {
        await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Subprocessos NOCHECK CONSTRAINT ALL");

        var subprocessosParaExcluir = await _context.Subprocessos
            .Where(s => s.ProceId == dto.Id)
            .ToListAsync();

        _context.Subprocessos.RemoveRange(subprocessosParaExcluir);
        await _context.SaveChangesAsync();

        await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Subprocessos CHECK CONSTRAINT ALL");

        Processo processo = await _context.Processos
            .Include(p => p.Ferramentas)
            .Include(p => p.Documentacoes)
            .FirstOrDefaultAsync(p => p.Id == dto.Id);

        if (processo == null)
        {
            throw new Exception("Processo não encontrado.");
        }

        // Remover as ferramentas e documentações associadas ao processo
        _context.ProcessoFerramentas.RemoveRange(processo.Ferramentas);
        _context.ProcessoDocumentacoes.RemoveRange(processo.Documentacoes);

        await _context.SaveChangesAsync();

        try
        {
            processo.Nome = dto.Nome;
            processo.Departamento = await _context.Departamentos.FirstOrDefaultAsync(r => r.Id == dto.DepartamentoId);

            processo.Subprocessos = ProcessarSubprocessos(dto.Id, dto.Subprocessos);

            processo.Ferramentas = dto.Ferramentas
                                    ?.Select(f => new ProcessoFerramenta
                                    {
                                        Ferramenta = new Ferramenta { Nome = f }
                                    })
                                    .ToList();

            processo.Responsaveis = await _context.Equipe.FirstOrDefaultAsync(r => r.Id == dto.Responsaveis);

            processo.Documentacoes = dto.Documentacoes
                                        ?.Select(doc => new ProcessoDocumentacao
                                        {
                                            Documentacao = new Documentacao
                                            {
                                                Nome = doc,
                                                Descricao = doc,
                                                URL = doc
                                            }
                                        })
                                        .ToList();

            _context.Update(processo);
            await _context.SaveChangesAsync();

        }
        catch
        {
            throw;
        }
    }

   
    private List<Subprocesso> ProcessarSubprocessos(int processoId, List<AddTsSubprocessoDto> dto)
    {
        var novosSubprocessos = HandleSubprocessos(dto);

        return novosSubprocessos;
    }

    private static List<Subprocesso> HandleSubprocessos(List<AddTsSubprocessoDto> subprocess, Subprocesso? subprocessoPai = null)
    {

        return subprocess.Select(sub =>
        {
            var subprocesso = new Subprocesso
            {
                Nome = sub.Nome,
                SubprocessoPai = subprocessoPai,
            };

            subprocesso.SubprocessosFilhos = sub.Subprocessos != null
            ? HandleSubprocessos(sub.Subprocessos, subprocesso)
            : null;

            return subprocesso;
        }).ToList();

    }
}


