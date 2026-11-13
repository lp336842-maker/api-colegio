using Colegio.Dominio.Entidades;
using Colegio.Infraestructura.Contexto;
using Colegio.Infraestructura.Nucleo;
using Colegio.Infraestructura.Interfaces;

namespace Colegio.Infraestructura.Repositorios;

public class RepositorioDocentes(ContextoColegio context) : RepositorioBase<Docente>(context), IRepositorioDocentes { }
