using Microsoft.AspNetCore.Mvc;
using Colegio.Dominio.Entidades;
using Colegio.Infraestructura.Interfaces;
using Colegio.Infraestructura.Modelos;

namespace Colegio.Api.Controllers;

[ApiController]
[Route("api/estudiantes")]
public class EstudiantesController(IRepositorioEstudiantes repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstudianteModelo>>> ObtenerTodos(CancellationToken ct) => Ok((await repository.ObtenerTodosAsync(ct)).Select(Map));
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EstudianteModelo>> ObtenerPorId(int id, CancellationToken ct)
    {
        var item = await repository.ObtenerPorIdAsync(id, ct);
        return item is null ? NotFound() : Ok(Map(item));
    }
    [HttpPost]
    public async Task<ActionResult<EstudianteModelo>> Crear(EstudianteModelo model, CancellationToken ct)
    {
        var item = await repository.AgregarAsync(new Estudiante { Nombre = model.Nombre, Apellido = model.Apellido, Correo = model.Correo, FechaInscripcion = model.FechaInscripcion }, ct);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = item.Id }, Map(item));
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, EstudianteModelo model, CancellationToken ct)
    {
        if (id != model.Id) return BadRequest();
        return await repository.ActualizarAsync(new Estudiante { Id = id, Nombre = model.Nombre, Apellido = model.Apellido, Correo = model.Correo, FechaInscripcion = model.FechaInscripcion }, ct) ? NoContent() : NotFound();
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id, CancellationToken ct) => await repository.EliminarAsync(id, ct) ? NoContent() : NotFound();
    private static EstudianteModelo Map(Estudiante x) => new(x.Id, x.Nombre, x.Apellido, x.Correo, x.FechaInscripcion);
}
