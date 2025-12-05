using System;
using System.Threading.Tasks;

namespace StoreModel.API.Test
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("===== PRUEBA DE PROVEEDORES =====");
            await ProveedoresTest.CrearActualizarEliminar();

            Console.WriteLine("\n===== PRUEBA DE CATEGORÍAS =====");
            await CategoriasTest.CrearActualizarEliminar();

            Console.WriteLine("\n===== PRUEBA DE PRODUCTOS =====");
            await ProductosTest.CrearActualizarEliminar(2);

            Console.WriteLine("\n===== PRUEBA DE MovimientoInventario =====");
            await MovimientoInventarioTest.CrearActualizarEliminar(2);
            Console.WriteLine("\n===== PRUEBA DE ProductoProveedor =====");
             ProductoProveedorTest.CrearActualizarEliminar();



        }
    }
}