using Microsoft.AspNetCore.Mvc;
using StageTest.Application.DTOs;
using StageTest.Application.Services;

namespace StageTest.Presentation.Controllers;
[Route("api/[controller]")]
[ApiController]

public class EquipesController : Controller
{
    private readonly EquipeService _equipeService;
    public EquipesController(EquipeService equipeService)
    {
        _equipeService = equipeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetEquipes()
    {
        try
        {
            return Ok(await _equipeService.GetAllEquipe());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEquipesDetail(int id)
    {
        try
        {
            return Ok(await _equipeService.GetEquipeDetail(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost()]
    public async Task<IActionResult> AddEquipes([FromBody] CreateEquipeDto dto)
    {
        try
        {
            await _equipeService.AddEquipe(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEquipes(int id, [FromBody] CreateEquipeDto dto)
    {
        try
        {
            await _equipeService.UpdateEquipe(id, dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEquipes(int id)
    {
        try
        {
            await _equipeService.DeleteEquipe(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
