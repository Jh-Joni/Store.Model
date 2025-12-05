using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Store.Model;

namespace StoreModel.API.Test
{
    internal class ProveedoresTest
    {
        private static readonly HttpClient httpClient =
            new HttpClient { BaseAddress = new Uri("https://store-model.onrender.com/api/") };

        public static async Task CrearActualizarEliminar()
        {
            
            // 1) CREAR PROVEEDOR
            
            var nuevo = new Proveedor
            {
                Nombre = "uguetelandia S.A.",
                Telefono = "123456789",
                Email = "proveedor@demo.com"
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(nuevo),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync("Proveedores", content);
            var json = await response.Content.ReadAsStringAsync();
            var creado = JsonConvert.DeserializeObject<Proveedor>(json);

            int id = creado?.Id ?? 0;

            Console.WriteLine("Proveedor Creado:");
            Console.WriteLine($"Id: {id}");
            Console.WriteLine($"Nombre: {creado?.Nombre}");
            Console.WriteLine($"Telefono: {creado?.Telefono}");
            Console.WriteLine($"Email: {creado?.Email}\n");

            if (id == 0)
            {
                Console.WriteLine("ERROR: La API no devolvió un ID válido.");
                return;
            }

            
            // 2) ACTUALIZAR PROVEEDOR
            
            var actualizado = new Proveedor
            {
                Id = id,
                Nombre = "uguetelandia S.A.",
                Telefono = "0991234567",
                Email = "contacto@juguetelandia.com"
            };

            content = new StringContent(
                JsonConvert.SerializeObject(actualizado),
                System.Text.Encoding.UTF8,
                "application/json");

            await httpClient.PutAsync($"Proveedores/{id}", content);

           
            // 3) CONSULTAR DATOS ACTUALIZADOS
           
            var responseGet = await httpClient.GetAsync($"Proveedores/{id}");
            var jsonGet = await responseGet.Content.ReadAsStringAsync();
            var proveedorFinal = JsonConvert.DeserializeObject<Proveedor>(jsonGet);

            Console.WriteLine("Proveedor Actualizado:");
            Console.WriteLine($"Id: {proveedorFinal?.Id}");
            Console.WriteLine($"Nombre: {proveedorFinal?.Nombre}");
            Console.WriteLine($"Telefono: {proveedorFinal?.Telefono}");
            Console.WriteLine($"Email: {proveedorFinal?.Email}\n");

            
            // 4) ELIMINAR PROVEEDOR
            
            await Eliminar(id);
        }

        private static async Task Eliminar(int id)
        {
            //var response = await httpClient.DeleteAsync($"Proveedores/{id}");
            //var json = await response.Content.ReadAsStringAsync();

            //Console.WriteLine("Proveedor Eliminado:");
            //Console.WriteLine($"Id Eliminado: {id}");
            //Console.WriteLine($"Respuesta API: {json}\n");
        }
    }
}
