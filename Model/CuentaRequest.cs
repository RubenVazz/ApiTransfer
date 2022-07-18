using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CuentaRequest
    {
        public string? NumeroCuenta { get; set; }
        public string? Moneda { get; set; }
        public string? Cedula { get; set; }
        public decimal Saldo { get; set; }
        public string CodigoBanco { get; set; }
    }
}
