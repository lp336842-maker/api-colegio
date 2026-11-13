using Microsoft.AspNetCore.Mvc;
using Colegio.Dominio.Entidades;
using Colegio.Infraestructura.Interfaces;
using Colegio.Infraestructura.Modelos;

namespace Colegio.Api.Controllers;

[ApiController]
[Route("api/cursos")]
public class CursosController(IRepositorioCursos repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CursoModelo>>> ObtenerTodos(CancellationToken ct) => Ok((await repository.ObtenerTodosAsync(ct)).Select(Map));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CursoModelo>> ObtenerPorId(int id, CancellationToken ct)
    {
        var item = await repository.ObtenerPorIdAsync(id, ct);
        return item is null ? NotFound() : Ok(Map(item));
    }

    [HttpPost]
    public async Task<ActionResult<CursoModelo>> Crear(CursoModelo model, CancellationToken ct)
    {
        var item = await repository.AgregarAsync(new Curso { Codigo = model.Codigo, Nombre = model.Nombre, Creditos = model.Creditos, DepartamentoId = model.DepartamentoId }, ct);
        var created = await repository.ObtenerPorIdAsync(item.Id, ct) ?? item;
        return CreatedAtAction(nameof(ObtenerPorId), new { id = item.Id }, Map(created));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, CursoModelo model, CancellationToken ct)
    {
        if (id != model.Id) return BadRequest();
        var item = new Curso { Id = id, Codigo = model.Codigo, Nombre = model.Nombre, Creditos = model.Creditos, DepartamentoId = model.DepartamentoId };
        return await repository.ActualizarAsync(item, ct) ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id, CancellationToken ct) => await repository.EliminarAsync(id, ct) ? NoContent() : NotFound();

    private static CursoModelo Map(Curso x) => new(x.Id, x.Codigo, x.Nombre, x.Creditos, x.DepartamentoId, x.Departamento?.Nombre);
}
