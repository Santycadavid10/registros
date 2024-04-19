using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Registros.Models;
using Registros.Data;
using System.Text.Json;
using Ingresos.Models;
using Usuarios.Models;


namespace Usuarios.Controllers;

public class UserController : Controller
{
  public readonly BaseContext _context;

  public UserController(BaseContext context)
  {
    _context = context;
  }





}







