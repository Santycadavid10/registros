namespace Registros.Models
{
  public class User
  {
    public int Id { get; set; }
    public string? nombre { get; set; }
    public string? documento_hash  { get; set; }


  }

    public class Registro
  {
    public int id { get; set; }
    
    public string? documento_usuario { get; set; }
    public DateTime 	hora_ingreso { get; set; }
    public DateTime 	hora_salida  { get; set; }
    

  }
}