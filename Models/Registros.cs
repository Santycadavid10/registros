namespace Registros.Models
{
    public class Registro
  {
    public int id { get; set; }
    public string? documento_usuario { get; set; }
    public DateTime 	hora_ingreso { get; set; }
    public DateTime 	hora_salida  { get; set; }
  }
}