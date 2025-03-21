using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StageTest.Domain.Entities;
using StageTest.Domain;
using StageTest.Application.DTOs;
using Microsoft.IdentityModel.Tokens;
using StageTest.Application.Services;

namespace StageTest.Presentation.Controllers;


[Route("api/[controller]")]
[ApiController]
public class DepartamentosController : Controller
{
    private readonly DepartamentoService _departamentoService;

    public DepartamentosController(DepartamentoService departamentoService)
    {
        _departamentoService = departamentoService;
    }

    [HttpPost()]
    public async Task<IActionResult> AddDepartamento([FromBody] DepartamentoDto dto)
    {
        try
        {
            return Ok(await _departamentoService.AddDepartamento(dto));
             

        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpPut()]
    public async Task<IActionResult> EditDepartamento([FromBody] DepartamentoDto dto)
    {
        try
        {
            await _departamentoService.UpdateDepartamento(dto);
            return Ok();


        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartamento(int id)
    {
        try
        {
            await _departamentoService.DeleteDepartamento(id);
            return Ok();


        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpGet()]
    public async Task<IActionResult> GetDepartamentos()
    {
        try
        {
            return Ok(await _departamentoService.GetAllAsync());
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }


}
