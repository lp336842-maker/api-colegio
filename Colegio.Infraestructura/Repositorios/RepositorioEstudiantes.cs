using Colegio.Dominio.Entidades;
using Colegio.Infraestructura.Contexto;
using Colegio.Infraestructura.Nucleo;
using Colegio.Infraestructura.Interfaces;

namespace Colegio.Infraestructura.Repositorios;

public class RepositorioEstudiantes(ContextoColegio context) : RepositorioBase<Estudiante>(context), IRepositorioEstudiantes { }
