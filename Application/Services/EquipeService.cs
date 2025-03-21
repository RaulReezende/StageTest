using Microsoft.AspNetCore.Mvc;
using StageTest.Application.DTOs;
using StageTest.Domain.Entities;
using StageTest.Infrastructure.Repositories;

namespace StageTest.Application.Services;

public class EquipeService
{
    private readonly IEquipeRepository _equipeRepository;
    private readonly IResponsavelRepository _pessoasRepository;
    public EquipeService(IEquipeRepository equipeRepository, IResponsavelRepository pessoasRepository)
    {
        _equipeRepository = equipeRepository;
        _pessoasRepository = pessoasRepository;
    }

    public Task<ResponseGetDetailEquipe> GetEquipeDetail(int equipeId)
    {
        return _equipeRepository.GetDetailEquipes(equipeId);
    }

    public async Task<IEnumerable<Equipe>> GetAllEquipe()
    {
        return await _equipeRepository.GetAllAsync();
    }

    public async Task AddEquipe(CreateEquipeDto dto)
    {
        try
        {
            Equipe equipe = new Equipe()
            {
                Nome = dto.Nome
            };

            var responsaveis = await _pessoasRepository.GetByIdsAsync(dto.Responsaveis);

            equipe.Responsaveis = responsaveis;
            await _equipeRepository.AddAsync(equipe);
            await _equipeRepository.SaveChangesAsync();

            //return equipe.EquipeId;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateEquipe(int id, CreateEquipeDto dto)
    {
        try
        {
            Equipe equipe = await _equipeRepository.GetByIdWithResponsaveisAsync(id)
                             ?? throw new Exception("Equipe não encontrada");

            equipe.Nome = dto.Nome;

            equipe.Responsaveis.Clear();

            var novosResponsaveis = await _pessoasRepository.GetByIdsAsync(dto.Responsaveis);
            foreach (var responsavel in novosResponsaveis)
            {
                equipe.Responsaveis.Add(responsavel);
            }

            await _equipeRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteEquipe(int idEquipe)
    {
        try
        {
            Equipe equipe = await _equipeRepository.GetByIdWithResponsaveisAsync(idEquipe) ?? throw new Exception("Equipe não encontrada");

            equipe.Responsaveis.Clear();

            _equipeRepository.Delete(equipe);
            await _equipeRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
