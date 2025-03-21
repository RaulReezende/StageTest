using Microsoft.AspNetCore.Mvc;
using StageTest.Application.DTOs;
using StageTest.Application.Services;

namespace StageTest.Presentation.Controllers;

[Route("api/responsaveis")]
[ApiController]
public class ResponsaveisController : Controller
{
    private readonly ResponsavelService _responsavelService;
    public ResponsaveisController(ResponsavelService ResponsaveisService)
    {
        _responsavelService = ResponsaveisService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetResponsaveis()
    {
        try
        {
            return Ok(await _responsavelService.GetResponsaveis());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{equipeId}")]
    public async Task<IActionResult> GetResponsaveisEquipe(int equipeId)
    {
        try
        {
            return Ok(await _responsavelService.GetResponsavelByEquipeId(equipeId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getresponsavel/{id}")]
    public async Task<IActionResult> GetResponsavel(int id)
    {
        try
        {
            return Ok(await _responsavelService.GetResponsavel(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    public async Task<IActionResult> AddResponsavel([FromBody] AddResponsavelDto dto)
    {
        try
        {
            return Ok(await _responsavelService.AddResponsavel(dto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateResponsavel(int id, [FromBody] AddResponsavelDto dto)
    {
        try
        {
            await _responsavelService.UpdateResponsavel(id, dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResponsavel(int id)
    {
        try
        {
            await _responsavelService.DeleteResponsavel(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
