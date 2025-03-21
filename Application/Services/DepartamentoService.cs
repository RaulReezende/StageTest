using StageTest.Application.DTOs;
using StageTest.Domain.Entities;
using StageTest.Infrastructure.Repositories;

namespace StageTest.Application.Services;

public class DepartamentoService
{
    private readonly IDepartamentoRepository _departamentoRepository;

    public DepartamentoService(IDepartamentoRepository departamentoRepository)
    {
        _departamentoRepository = departamentoRepository;
    }


    public async Task<IEnumerable<Departamento>> GetAllAsync()
    {
        try
        {
            return await _departamentoRepository.GetAllAsync();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Departamento> AddDepartamento(DepartamentoDto dto)
    {
        try
        {
            if (string.IsNullOrEmpty(dto.Nome))
                throw new Exception("Departamento vazio");


            Departamento departamento = new()
            {
                Nome = dto.Nome,
            };


            await _departamentoRepository.AddAsync(departamento);
            await _departamentoRepository.SaveChangesAsync();
            return departamento;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateDepartamento(DepartamentoDto dto)
    {
        try
        {
            Departamento departamento = await _departamentoRepository.GetByIdAsync(dto.Id) ?? throw new Exception("Departamento não encontrado");
            departamento.Nome = dto.Nome;
            _departamentoRepository.Update(departamento);
            await _departamentoRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteDepartamento(int id)
    {
        try
        {
            Departamento departamento = await _departamentoRepository.GetByIdAsync(id) ?? throw new Exception("Departamento não encontrado");

            _departamentoRepository.Delete(departamento);
            await _departamentoRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    
}
