using Colegio.Dominio.Nucleo;

namespace Colegio.Infraestructura.Nucleo;

public interface IRepositorio<T> where T : EntidadBase
{
    Task<IReadOnlyList<T>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
    Task<T?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);
    Task<T> AgregarAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> ActualizarAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(int id, CancellationToken cancellationToken = default);
}
