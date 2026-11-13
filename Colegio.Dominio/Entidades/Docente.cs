using System.ComponentModel.DataAnnotations;
using Colegio.Dominio.Nucleo;

namespace Colegio.Dominio.Entidades;

public class Docente : EntidadBase
{
    [Required, StringLength(80)]
    public string Nombre { get; set; } = string.Empty;
    [Required, StringLength(80)]
    public string Apellido { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(160)]
    public string Correo { get; set; } = string.Empty;
    public int DepartamentoId { get; set; }
    public Departamento? Departamento { get; set; }
    public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
