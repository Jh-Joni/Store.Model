using Newtonsoft.Json;
using Store.Model;

using System.Text;

namespace StoreModel.API.Test
{
    public class ProductoProveedorTest
    {
        private static readonly HttpClient http = new HttpClient()
        {
            BaseAddress = new Uri("https://store-model.onrender.com/api/")
        };

        public static void CrearActualizarEliminar()
        {
            Console.WriteLine("===== PRUEBA DE PRODUCTO-PROVEEDOR =====");

            //  1) CREAR 
            var nuevo = new ProductoProveedor
            {
                ProductoId = 4,     
                ProveedorId = 2     
            };

            string jsonNuevo = JsonConvert.SerializeObject(nuevo);
            var contentNuevo = new StringContent(jsonNuevo, Encoding.UTF8, "application/json");

            var responseNuevo = http.PostAsync("api/ProductoProveedor", contentNuevo).Result;
            string jsonRespNuevo = responseNuevo.Content.ReadAsStringAsync().Result;

            var apiResultNuevo = JsonConvert.DeserializeObject<ApiResult<ProductoProveedor>>(jsonRespNuevo);
            int id = apiResultNuevo?.Data?.Id ?? 0;

            Console.WriteLine("CREADO correctamente con ID: " + id);

            //  2) ACTUALIZAR 
            var actualizar = new ProductoProveedor
            {
                Id = id,
                ProductoId = 4,   // Solo el producto con id 4
                ProveedorId = 2   
            };

            string jsonAct = JsonConvert.SerializeObject(actualizar);
            var contentAct = new StringContent(jsonAct, Encoding.UTF8, "application/json");

            var responseAct = http.PutAsync($"api/ProductoProveedor/{id}", contentAct).Result;
            string jsonRespAct = responseAct.Content.ReadAsStringAsync().Result;

            Console.WriteLine("ACTUALIZADO correctamente ID: " + id);

            // ========== 3) OBTENER TODOS LOS PRODUCTOS-PROVEEDOR CON DETALLES ==========
            var responseAll = http.GetAsync("api/ProductoProveedor").Result;
            string jsonRespAll = responseAll.Content.ReadAsStringAsync().Result;
            var apiResultAll = JsonConvert.DeserializeObject<ApiResult<List<ProductoProveedor>>>(jsonRespAll);

            Console.WriteLine("===== DETALLE DE PRODUCTO-PROVEEDOR (ProductoId = 4) =====");
            if (apiResultAll?.Data != null)
            {
                foreach (var pp in apiResultAll.Data)
                {
                    if (pp.ProductoId == 4)
                    {
                        Console.WriteLine($"ID: {pp.Id}");
                        Console.WriteLine($"Producto: {pp.Producto?.Nombre}");
                        Console.WriteLine($"Descripción: {pp.Producto?.Descripcion}");
                        Console.WriteLine($"Precio: {pp.Producto?.Precio}");
                        Console.WriteLine($"Stock: {pp.Producto?.Stock}");
                        Console.WriteLine($"Proveedor: {pp.Proveedor?.Nombre}");
                        Console.WriteLine($"Teléfono Proveedor: {pp.Proveedor?.Telefono}");
                        Console.WriteLine($"Email Proveedor: {pp.Proveedor?.Email}");
                        Console.WriteLine("----------------------------------------");
                    }
                }
            }

            // ========== 4) ELIMINAR ==========
            //var responseDel = http.DeleteAsync($"api/ProductoProveedor/{id}").Result;
            //string respDel = responseDel.Content.ReadAsStringAsync().Result;

            //Console.WriteLine("ELIMINADO correctamente ID: " + id);
        }
    }
}
