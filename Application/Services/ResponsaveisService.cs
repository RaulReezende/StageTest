using StageTest.Application.DTOs;
using StageTest.Domain.Entities;
using StageTest.Infrastructure.Repositories;

namespace StageTest.Application.Services;

public class ResponsavelService
{
    private readonly IResponsavelRepository _responsavelRepository;
    public ResponsavelService(IResponsavelRepository pessoasRepository)
    {
        _responsavelRepository = pessoasRepository;
    }

    public async Task<List<Responsavel>> GetResponsaveis()
    {
        return await _responsavelRepository.GetResponsaveis();
    }

    public async Task<List<Responsavel>> GetResponsavelByEquipeId(int equipeId)
    {
        return await _responsavelRepository.GetResponsavelByEquipeId(equipeId);
    }

    public async Task<Responsavel> GetResponsavel(int id)
    {
        return await _responsavelRepository.GetByIdAsync(id);
    }

    public async Task<int> AddResponsavel(AddResponsavelDto dto)
    {
        try
        {
            Responsavel responsavel = new Responsavel()
            {
                Nome = dto.Nome,
                Cargo = dto.Cargo,
                Email = dto.Email,
            };

            await _responsavelRepository.AddAsync(responsavel);
            await _responsavelRepository.SaveChangesAsync();
            return responsavel.Id;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task UpdateResponsavel(int id, AddResponsavelDto dto)
    {
        try
        {
            Responsavel responsavel = await _responsavelRepository.GetByIdAsync(id) ?? throw new Exception("Responsável não encontrado");

            responsavel.Nome = dto.Nome;
            responsavel.Cargo = dto.Cargo;
            responsavel.Email = dto.Email;

             _responsavelRepository.Update(responsavel);
            await _responsavelRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteResponsavel(int pessoaId)
    {
        try
        {
            Responsavel responsavel = await _responsavelRepository.GetByIdAsync(pessoaId) ?? throw new Exception("Responsável não encontrado");
            _responsavelRepository.Delete(responsavel);
            await _responsavelRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    
}
