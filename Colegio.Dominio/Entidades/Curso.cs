using System.ComponentModel.DataAnnotations;
using Colegio.Dominio.Nucleo;

namespace Colegio.Dominio.Entidades;

public class Curso : EntidadBase
{
    [Required, StringLength(20)]
    public string Codigo { get; set; } = string.Empty;
    [Required, StringLength(150)]
    public string Nombre { get; set; } = string.Empty;
    [Range(1, 20)]
    public int Creditos { get; set; }
    public int DepartamentoId { get; set; }
    public Departamento? Departamento { get; set; }
    public ICollection<Docente> Docentes { get; set; } = new List<Docente>();
    public ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}
