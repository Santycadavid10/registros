using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Registros.Models;
using Registros.Data;
using System.Text.Json;
using Ingresos.Models;
using Microsoft.EntityFrameworkCore;

namespace Ingresos.Controllers;
public class AdministradoresController : Controller{
    public readonly BaseContext _context;
    public AdministradoresController(BaseContext context)
    {
        _context = context;
    }
    //Controlador para ver usuarios
    public async Task<IActionResult> AdminView(){
        return View(await _context.Users.ToListAsync());
    }
    //Controlador para ver detalles
    public async Task<IActionResult> Detalles(int? Documento){
        if (Documento == null)
        {
            return NotFound();
        }
        string? idstring = Documento.ToString();
        var Registros = await _context.Registros.Where( m => m.documento_usuario == idstring).ToListAsync();
        if (Registros == null)
        {
            return NotFound();
        }
        return View(Registros);
    }
}