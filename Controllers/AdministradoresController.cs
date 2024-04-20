using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Registros.Models;
using Registros.Data;
using System.Text.Json;
using Ingresos.Models;
using Microsoft.EntityFrameworkCore;
using Usuarios.Models;

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
    //Controlador para eliminar
    public async Task<IActionResult> Eliminar (int? id){
        var user = await _context.Users.FindAsync(id);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return RedirectToAction("AdminView");
    }

    //Controlador para crear
    public IActionResult Crear(){
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Crear([Bind("nombre, documento_hash ")]User user){
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminView");
        }
        return View(user);
    }
}