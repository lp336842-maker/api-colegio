using Microsoft.EntityFrameworkCore;
using Colegio.Dominio.Entidades;
using Colegio.Infraestructura.Contexto;
using Colegio.Infraestructura.Nucleo;
using Colegio.Infraestructura.Interfaces;

namespace Colegio.Infraestructura.Repositorios;

public class RepositorioCursos(ContextoColegio context) : RepositorioBase<Curso>(context), IRepositorioCursos
{
    public override async Task<IReadOnlyList<Curso>> ObtenerTodosAsync(CancellationToken cancellationToken = default) => await Set.Include(x => x.Departamento).AsNoTracking().ToListAsync(cancellationToken);
    public override Task<Curso?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default) => Set.Include(x => x.Departamento).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
}
