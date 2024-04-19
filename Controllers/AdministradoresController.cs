using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Registros.Models;
using Registros.Data;
using System.Text.Json;
using Ingresos.Models;

namespace Ingresos.Controllers;
public class AdministradoresController : Controller{
    public readonly BaseContext _context;
    public AdministradoresController(BaseContext context)
    {
        _context = context;
    }
    public IActionResult AdminView(){
    return View();
    }
    public IActionResult Verify (string Nombre, string Documento){
        //Verfica si el usuario es Admin
        var usuarioEncontrado = _context.Users.FirstOrDefault(u => u.nombre == Nombre && u.documento_hash == Documento);
        if (usuarioEncontrado.nombre == "admin" && usuarioEncontrado.documento_hash == "admin123"){
            return RedirectToAction ("AdminView", "Administradores");
        }

        else
        {
            return RedirectToAction("Index", "Home");
        }
    }
}