using Newtonsoft.Json;
using Store.Model;

using System.Text;

namespace StoreModel.API.Test
{
    public class ProductoProveedorTest
    {
        private static readonly HttpClient http = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7167/")
        };

        public static void CrearActualizarEliminar()
        {
            Console.WriteLine("===== PRUEBA DE PRODUCTO-PROVEEDOR =====");

            //  1) CREAR 
            var nuevo = new ProductoProveedor
            {
                ProductoId = 2,     
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
                ProductoId = 2,   
                ProveedorId = 2   
            };

            string jsonAct = JsonConvert.SerializeObject(actualizar);
            var contentAct = new StringContent(jsonAct, Encoding.UTF8, "application/json");

            var responseAct = http.PutAsync($"api/ProductoProveedor/{id}", contentAct).Result;
            string jsonRespAct = responseAct.Content.ReadAsStringAsync().Result;

            Console.WriteLine("ACTUALIZADO correctamente ID: " + id);

            // ========== 3) ELIMINAR ==========
            var responseDel = http.DeleteAsync($"api/ProductoProveedor/{id}").Result;
            string respDel = responseDel.Content.ReadAsStringAsync().Result;

            Console.WriteLine("ELIMINADO correctamente ID: " + id);
        }
    }
}
