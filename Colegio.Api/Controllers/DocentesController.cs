using Microsoft.AspNetCore.Mvc;
using Colegio.Dominio.Entidades;
using Colegio.Infraestructura.Interfaces;
using Colegio.Infraestructura.Modelos;

namespace Colegio.Api.Controllers;

[ApiController]
[Route("api/docentes")]
public class DocentesController(IRepositorioDocentes repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DocenteModelo>>> ObtenerTodos(CancellationToken ct) => Ok((await repository.ObtenerTodosAsync(ct)).Select(Map));
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DocenteModelo>> ObtenerPorId(int id, CancellationToken ct)
    {
        var item = await repository.ObtenerPorIdAsync(id, ct);
        return item is null ? NotFound() : Ok(Map(item));
    }
    [HttpPost]
    public async Task<ActionResult<DocenteModelo>> Crear(DocenteModelo model, CancellationToken ct)
    {
        var item = await repository.AgregarAsync(new Docente { Nombre = model.Nombre, Apellido = model.Apellido, Correo = model.Correo, DepartamentoId = model.DepartamentoId }, ct);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = item.Id }, Map(item));
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, DocenteModelo model, CancellationToken ct)
    {
        if (id != model.Id) return BadRequest();
        return await repository.ActualizarAsync(new Docente { Id = id, Nombre = model.Nombre, Apellido = model.Apellido, Correo = model.Correo, DepartamentoId = model.DepartamentoId }, ct) ? NoContent() : NotFound();
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id, CancellationToken ct) => await repository.EliminarAsync(id, ct) ? NoContent() : NotFound();
    private static DocenteModelo Map(Docente x) => new(x.Id, x.Nombre, x.Apellido, x.Correo, x.DepartamentoId);
}
