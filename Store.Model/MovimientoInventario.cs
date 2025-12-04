using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Model
{
    public class MovimientoInventario
    {
        [Key]
        public int Id { get; set; }
        public string Tipo { get; set; } // es para saber si la mercaderia entra o sale
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
       

        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }
    }
}
