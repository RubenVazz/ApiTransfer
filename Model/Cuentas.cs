namespace Model
{
    public class Cuentas
    {
        public string IdCuenta { get; set; } 
        public string? NumeroCuenta { get; set; }
        public string? Moneda { get; set; }
        public string? Cedula  { get; set; }
        public decimal Saldo { get; set; }
        public string CodigoBanco { get; set; }
    }
}