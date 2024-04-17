using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Ingresos.Models;
using Registros.Models;
using Registros.Data;

namespace Ingresos.Controllers;

public class HomeController : Controller
{
    public readonly BaseContext _context;

    public HomeController(BaseContext context)
    {
         _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }


public IActionResult verificado(int id)
{
    var usuarioEncontrado = _context.Users.FirstOrDefault(u => u.Id == id);

    if (usuarioEncontrado != null)
    {
        return View(usuarioEncontrado);
    }
    else
    {
        // Manejar el caso en que no se encuentra el usuario
        return RedirectToAction("UsuarioNoEncontrado");
    }
}






    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

[HttpPost]
    public IActionResult Verify(string Nombre, string Documento)
    {
     // Verificar si el usuario y la contraseña coinciden en la base de datos
    var usuarioEncontrado = _context.Users.FirstOrDefault(u => u.nombre == Nombre && u.documento_hash == Documento);
        if (usuarioEncontrado != null)
        {
           return RedirectToAction("verificado", "Home", new { id = usuarioEncontrado.Id }); // Puedes pasar el ID u otro parámetro si lo necesitas
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }

}
