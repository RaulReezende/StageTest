using Microsoft.AspNetCore.Mvc;
using StageTest.Application.DTOs;
using StageTest.Application.Services;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace StageTest.Presentation.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProcessosController : Controller
{
    private readonly ProcessoService _processoService;
    public ProcessosController(ProcessoService processoService)
    {
        _processoService = processoService;
    }

    [HttpGet("{departamentoId}")]
    public async Task<IActionResult> GetAllAsync(int departamentoId)
    {
        try
        {
            return Ok(await _processoService.GetAllAsync(departamentoId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getprocesso/{processoId}")]
    public async Task<IActionResult> GetProcesso(int processoId)
    {
        try
        {
            return Ok(await _processoService.GetProcesso(processoId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    public async Task<IActionResult> AddProcesso(AddProcessDtoTest dto)
    {
        try
        {
            await _processoService.AddProcesso(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProcesso([FromBody] AddProcessDtoTest dto)
    {
        try
        {
                await _processoService.UpdateProcesso(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProcesso(int id)
    {
        try
        {
            await _processoService.DeleteProcesso(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
public class Processo
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<Processo> Subprocessos { get; set; }
}