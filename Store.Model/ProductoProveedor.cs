using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Model
{
    public class ProductoProveedor
    {
        public int Id { get; set; }
        public bool Activo { get; set; } 

        public DateTime FechaCreacion { get; set; }

        public int ProductoId { get; set; }
        public int ProveedorId { get; set; }
        public Producto? Producto { get; set; }
        public Proveedor? Proveedor { get; set; }
    }
}
