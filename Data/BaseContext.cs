using Microsoft.EntityFrameworkCore;
using Registros.Models;
using Usuarios.Models;



namespace Registros.Data
{
  public class BaseContext : DbContext
  {
    public BaseContext(DbContextOptions<BaseContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Registro> Registros { get; set; }
  }
}