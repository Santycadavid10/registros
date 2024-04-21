using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Registros.Data;
using Registros.Models;
using Usuarios.Models;
using Ingresos.Models;



namespace Ingresos.Controllers;

public class HomeController : Controller
{
  public readonly BaseContext _context;

  public HomeController(BaseContext context)
  {
    _context = context;
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }








  public IActionResult Index()
  {
    //Response.Cookies.Delete("Registro");
    return View();
  }

  public IActionResult Privacy()
  {
    return View();
  }

   public IActionResult verificado2()
  {
    //Response.Cookies.Delete("Registro");
    return View();
  }
  public async Task<IActionResult> guardaregistro([Bind("documento_usuario, hora_ingreso, hora_salida")] Registro registro)
  {
    if (ModelState.IsValid)
    {
      _context.Registros.Add(registro);
      await _context.SaveChangesAsync();

    }
    return RedirectToAction("verificado2", "Home");
  }

  [HttpPost]
  public IActionResult Verify(string Nombre, string Documento)
  {
    // Verificar si el usuario y la contraseña coinciden en la base de datos
    var usuarioEncontrado = _context.Users.FirstOrDefault(u => u.nombre == Nombre && u.documento_hash == Documento);
    if (usuarioEncontrado != null)
    {
      if (usuarioEncontrado.nombre == "admin" && usuarioEncontrado.documento_hash == "admin123")
      {
        return RedirectToAction("AdminView", "Administradores");
      }
      else{
        return RedirectToAction("verificado", "Home", new { id = usuarioEncontrado.Id }); // Puedes pasar el ID u otro parámetro si lo necesitas
      }
      
    }
    else
    {
      return RedirectToAction("Index", "Home");
    }
  }

  public IActionResult Verificado(int id)
  {
    var usuarioEncontrado = _context.Users.FirstOrDefault(u => u.Id == id);


    TempData["documento"] = usuarioEncontrado?.documento_hash;
    TempData["hora"] = DateTime.Now;
    TempData["salida"] = null;
    var movimiento = new Registro // Asume que 'Registro' es el nombre de tu modelo de movimiento
    {

      documento_usuario = usuarioEncontrado?.documento_hash,
      hora_ingreso = DateTime.Now
    };

    if (!HttpContext.Request.Cookies.ContainsKey("Registro"))
    {
      // Si la cookie no existe, crearla y establecer el valor
      string RegistroJson = JsonSerializer.Serialize(new List<Registro> { movimiento });
      HttpContext.Response.Cookies.Append("Registro", RegistroJson);

    }
    else
    {
      // Si la cookie ya existe, obtener su valor y deserializarlo
      string? RegistroJson = HttpContext.Request.Cookies["Registro"];
      #nullable disable
      List<Registro> listaRegistro = JsonSerializer.Deserialize<List<Registro>>(RegistroJson);
      #nullable disable
      
      foreach (var item in listaRegistro)
      {
        if (item.documento_usuario == movimiento.documento_usuario)
        {
          var registro = new Registro // Asume que 'Registro' es el nombre de tu modelo de movimiento
          {
            documento_usuario = usuarioEncontrado.documento_hash,
            hora_ingreso = item.hora_ingreso,
            hora_salida = DateTime.Now
          };
          TempData["hora"] = item.hora_ingreso;
          TempData["salida"] = DateTime.Now;
          listaRegistro.Remove(item);
          Response.Cookies.Delete("Registro");
          string RegistroJson1 = JsonSerializer.Serialize(listaRegistro);
          HttpContext.Response.Cookies.Append("Registro", RegistroJson1);
          return RedirectToAction("guardaregistro", registro);
        }

      }

      // Agregar el nuevo movimiento a la lista existente
      listaRegistro.Add(movimiento);




      // Serializar la lista actualizada y guardarla en la cookie
      string RegistroJsonActualizado = JsonSerializer.Serialize(listaRegistro);
      HttpContext.Response.Cookies.Append("Registro", RegistroJsonActualizado);
      ViewData["Registro"] = RegistroJsonActualizado;
    }

    // Continuar con tu lógica de controlador...
    return View();
  }
}










