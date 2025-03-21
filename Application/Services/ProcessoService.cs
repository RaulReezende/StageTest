using Microsoft.AspNetCore.SignalR;
using StageTest.Application.DTOs;
using StageTest.Domain.Entities;
using StageTest.Infrastructure.Repositories;
using StageTest.Infrastructure.Repositories.SubProcessos;

namespace StageTest.Application.Services;

public class ProcessoService
{
    private readonly IProcessosRepository _processoRepository;
    private readonly IEquipeRepository _equipeRepository;
    private readonly IDepartamentoRepository _departamentoRepository;
    private readonly ISubProcessoRepository _subprocessoRepository;

    public ProcessoService(IProcessosRepository processoRepository, IEquipeRepository equipeRepository, IDepartamentoRepository departamentoRepository, ISubProcessoRepository subProcessoRepository)
    {
        _processoRepository = processoRepository;
        _equipeRepository = equipeRepository;
        _departamentoRepository = departamentoRepository;
        _subprocessoRepository = subProcessoRepository;

    }
    public async Task<List<ResponseAllProcesso>> GetAllAsync(int DepartamentoId)
    {
        try
        {
            return await _processoRepository.GetAll(DepartamentoId);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ResponseAllProcesso> GetProcesso(int DepartamentoId)
    {
        try
        {
            return await _processoRepository.GetProcesso(DepartamentoId);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task AddProcesso(AddProcessDtoTest dto)
    {
        try
        {
            if (dto == null || string.IsNullOrEmpty(dto.Nome))
                throw new Exception("Processo vazio");

            Equipe responsavel = await _equipeRepository.GetByIdAsync(dto.Responsaveis) ?? throw new Exception("Responsável não encontrado");
            Departamento departamento = await _departamentoRepository.GetByIdAsync(dto.DepartamentoId) ?? throw new Exception("Responsável não encontrado");
            
            Processo processo = HandleProcessos(dto, responsavel, departamento);
            await _processoRepository.AddAsync(processo);
            await _processoRepository.SaveChangesAsync();

            if (dto.Subprocessos != null)
            {
                List<Subprocesso> subprocessos = HandleSubprocessos(dto.Subprocessos);
                List<Subprocesso> novo = AddIdSubprocesso(subprocessos, processo.Id);
                processo.Subprocessos = novo;
                _processoRepository.Update(processo);
                await _subprocessoRepository.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public List<Subprocesso> AddIdSubprocesso(List<Subprocesso> subprocessos, int processoId)
    {
        foreach(var sub in subprocessos)
    {
            // Define o ProceId no objeto atual
            sub.ProceId = processoId;

            // Chamada recursiva para os filhos
            if (sub.SubprocessosFilhos != null && sub.SubprocessosFilhos.Any())
            {
                AddIdSubprocesso(sub.SubprocessosFilhos.ToList(), processoId);
            }
        }
        return subprocessos;
    }

    public async Task UpdateProcesso(AddProcessDtoTest dto)
    {
        try
        {
            await _subprocessoRepository.DeleteAllSubprocessos(dto.Id);
            await _subprocessoRepository.DeleteProcesso(dto.Id);
            await AddProcesso(dto);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteProcesso(int id)
    {
        try
        {
            await _subprocessoRepository.DeleteAllSubprocessos(id);
            await _subprocessoRepository.DeleteProcesso(id);


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static Processo HandleProcessos(AddProcessDtoTest dto, Equipe responsavel, Departamento departamento)
    {

        return 
                 new Processo
                {
                    Nome = dto.Nome,
                    Departamento = departamento,
                     Ferramentas = dto.Ferramentas
                    ?.Select(f => new ProcessoFerramenta
                    {
                        Ferramenta = new Ferramenta { Nome = f }
                    })
                    .ToList(),
                    Responsaveis = responsavel,
                    Documentacoes = dto.Documentacoes
                    ?.Select(doc => new ProcessoDocumentacao
                    {
                        Documentacao = new Documentacao
                        {
                            Nome = doc,
                            Descricao = doc,
                            URL = doc
                        }
                    })
                    .ToList()
                };
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
