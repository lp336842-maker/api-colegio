using System.ComponentModel.DataAnnotations;
using Colegio.Dominio.Nucleo;

namespace Colegio.Dominio.Entidades;

public class Departamento : EntidadBase
{
    [Required, StringLength(100)]
    public string Nombre { get; set; } = string.Empty;
    [StringLength(300)]
    public string? Descripcion { get; set; }
    public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
    public ICollection<Docente> Docentes { get; set; } = new List<Docente>();
}
