using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Store.Model;

namespace StoreModel.API.Test
{
    internal class CategoriasTest
    {
        private static readonly HttpClient httpClient =
            new HttpClient { BaseAddress = new Uri("https://store-model.onrender.com/api/") };

        public static async Task CrearActualizarEliminar()
        {
            // 1) CREAR CATEGORÍA
            
            var nueva = new Categoria
            {
                Nombre = "Categoría Inicial"
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(nueva),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync("Categorias", content);
            var json = await response.Content.ReadAsStringAsync();
            var creada = JsonConvert.DeserializeObject<Categoria>(json);

            int id = creada?.Id ?? 0;

            Console.WriteLine("Categoría Creada:");
            Console.WriteLine($"Id: {id}");
            Console.WriteLine($"Nombre: {creada?.Nombre}\n");

            if (id == 0)
            {
                Console.WriteLine("ERROR: La API no devolvió un ID válido.");
                return;
            }

            
            // 2) ACTUALIZAR CATEGORÍA
            
            var actualizada = new Categoria
            {
                Id = id,
                Nombre = "Juguetes"
            };

            content = new StringContent(
                JsonConvert.SerializeObject(actualizada),
                System.Text.Encoding.UTF8,
                "application/json");

            await httpClient.PutAsync($"Categorias/{id}", content);

            // 3) CONSULTAR DATOS ACTUALIZADOS
            
            var responseGet = await httpClient.GetAsync($"Categorias/{id}");
            var jsonGet = await responseGet.Content.ReadAsStringAsync();
            var categoriaFinal = JsonConvert.DeserializeObject<Categoria>(jsonGet);

            Console.WriteLine("Categoría Actualizada:");
            Console.WriteLine($"Id: {categoriaFinal?.Id}");
            Console.WriteLine($"Nombre: {categoriaFinal?.Nombre}\n");

           
            // 4) ELIMINAR CATEGORÍA
           
            await Eliminar(id);
        }

        private static async Task Eliminar(int id)
        {
            //var response = await httpClient.DeleteAsync($"Categorias/{id}");
            //var json = await response.Content.ReadAsStringAsync();

            //Console.WriteLine("Categoría Eliminada:");
            //Console.WriteLine($"Id Eliminado: {id}");
            //Console.WriteLine($"Respuesta API: {json}\n");
        }
    }
}
