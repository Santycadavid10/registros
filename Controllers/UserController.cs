using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Registros.Data;
using Registros.Models;
using Usuarios.Models;

namespace Usuarios.Controllers
{
  public class UserController : Controller
  {
    public readonly BaseContext _context;
    public UserController(BaseContext context)
    { 
      _context = context;
    }
    public async Task<IActionResult> Detalles(int? id)
    {
      if(id == null)
      {
        return NotFound();
      }
      string idString = id.ToString();
      var registro =  await  _context.Registros.Where(m => m.documento_usuario == idString).ToListAsync();;
      if(registro == null)
      {
        return NotFound();
      }
      return View(registro);
    }
  }
}


