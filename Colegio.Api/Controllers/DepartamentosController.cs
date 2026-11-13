using Microsoft.AspNetCore.Mvc;
using Colegio.Dominio.Entidades;
using Colegio.Infraestructura.Interfaces;
using Colegio.Infraestructura.Modelos;

namespace Colegio.Api.Controllers;

[ApiController]
[Route("api/departamentos")]
public class DepartamentosController(IRepositorioDepartamentos repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartamentoModelo>>> ObtenerTodos(CancellationToken ct) => Ok((await repository.ObtenerTodosAsync(ct)).Select(Map));
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DepartamentoModelo>> ObtenerPorId(int id, CancellationToken ct)
    {
        var item = await repository.ObtenerPorIdAsync(id, ct);
        return item is null ? NotFound() : Ok(Map(item));
    }
    [HttpPost]
    public async Task<ActionResult<DepartamentoModelo>> Crear(DepartamentoModelo model, CancellationToken ct)
    {
        var item = await repository.AgregarAsync(new Departamento { Nombre = model.Nombre, Descripcion = model.Descripcion }, ct);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = item.Id }, Map(item));
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, DepartamentoModelo model, CancellationToken ct)
    {
        if (id != model.Id) return BadRequest();
        return await repository.ActualizarAsync(new Departamento { Id = id, Nombre = model.Nombre, Descripcion = model.Descripcion }, ct) ? NoContent() : NotFound();
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id, CancellationToken ct) => await repository.EliminarAsync(id, ct) ? NoContent() : NotFound();
    private static DepartamentoModelo Map(Departamento x) => new(x.Id, x.Nombre, x.Descripcion);
}
