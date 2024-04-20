using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using Registros.Data;
using System.Text.Json;
using Ingresos.Models;



namespace Usuary.Controllers;

public class UserController : Controller
{
  public readonly BaseContext _context;

  public UserController(BaseContext context)
  {
    _context = context;
  }





}







