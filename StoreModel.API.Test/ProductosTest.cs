using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Store.Model;

namespace StoreModel.API.Test
{
    internal class ProductosTest
    {
        private static readonly HttpClient httpClient =
            new HttpClient { BaseAddress = new Uri("https://store-model.onrender.com/api/") };

        
        public static async Task CrearActualizarEliminar(int categoriaId = 1)
        {
            
            var chkCat = await httpClient.GetAsync($"Categorias/{categoriaId}");
            if (!chkCat.IsSuccessStatusCode)
            {
                Console.WriteLine($"ERROR: La categoría con Id {categoriaId} no existe. Código: {chkCat.StatusCode}");
                var texto = await chkCat.Content.ReadAsStringAsync();
                Console.WriteLine($"Respuesta API: {texto}");
                return;
            }

            
            // 1) CREAR PRODUCTO
            
            var nuevo = new Producto
            {
                Nombre = "Bloques de construcción",
                Descripcion = "Juego de bloques plásticos para niños de 3 a 6 años",
                Precio = 18.50,
                Stock = 30,
                CategoriaId = 5
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(nuevo),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync("Productos", content);
            var json = await response.Content.ReadAsStringAsync();

            
            if (string.IsNullOrWhiteSpace(json) || json.TrimStart()[0] != '{')
            {
                Console.WriteLine("Error al crear producto. Respuesta no es JSON del producto:");
                Console.WriteLine(json);
                return;
            }

            Producto creado;
            try
            {
                creado = JsonConvert.DeserializeObject<Producto>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al deserializar la respuesta de creación:");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Respuesta cruda:");
                Console.WriteLine(json);
                return;
            }

            int id = creado?.Id ?? 0;

            Console.WriteLine("Producto Creado:");
            Console.WriteLine($"Id: {id}");
            Console.WriteLine($"Nombre: {creado?.Nombre}");
            Console.WriteLine($"Descripción: {creado?.Descripcion}");
            Console.WriteLine($"Precio: {creado?.Precio}");
            Console.WriteLine($"Stock: {creado?.Stock}");
            Console.WriteLine($"CategoriaId: {creado?.CategoriaId}\n");

            if (id == 0)
            {
                Console.WriteLine("ERROR: La API no devolvió un ID válido al crear el producto.");
                return;
            }

           
            // 2) ACTUALIZAR PRODUCTO
            var actualizado = new Producto
            {
                Nombre = "Bloques de construcción",
                Descripcion = "Juego de bloques plásticos para niños de 3 a 6 años",
                Precio = 18.50,
                Stock = 30,
                CategoriaId = 5
            };

            content = new StringContent(
                JsonConvert.SerializeObject(actualizado),
                System.Text.Encoding.UTF8,
                "application/json");

            var respPut = await httpClient.PutAsync($"Productos/{id}", content);
            var jsonPut = await respPut.Content.ReadAsStringAsync();

            Producto actualizadoResp = null;
            if (!string.IsNullOrWhiteSpace(jsonPut) && jsonPut.TrimStart()[0] == '{')
            {
                try
                {
                    actualizadoResp = JsonConvert.DeserializeObject<Producto>(jsonPut);
                }
                catch
                {
                    actualizadoResp = null;
                }
            }

            // 3) CONSULTAR PRODUCTO ACTUALIZADO 
            var responseGet = await httpClient.GetAsync($"Productos/{id}");
            var jsonGet = await responseGet.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(jsonGet) || jsonGet.TrimStart()[0] != '{')
            {
                Console.WriteLine("Error al obtener producto actualizado. Respuesta no es JSON:");
                Console.WriteLine(jsonGet);
                return;
            }

            Producto final;
            try
            {
                final = JsonConvert.DeserializeObject<Producto>(jsonGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al deserializar el GET del producto:");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Respuesta cruda:");
                Console.WriteLine(jsonGet);
                return;
            }

            Console.WriteLine("Producto Actualizado:");
            Console.WriteLine($"Id: {final?.Id}");
            Console.WriteLine($"Nombre: {final?.Nombre}");
            Console.WriteLine($"Descripción: {final?.Descripcion}");
            Console.WriteLine($"Precio: {final?.Precio}");
            Console.WriteLine($"Stock: {final?.Stock}");
            Console.WriteLine($"CategoriaId: {final?.CategoriaId}\n");

          
            // 4) ELIMINAR PRODUCTO
           
            //var responseDel = await httpClient.DeleteAsync($"Productos/{id}");
            //var jsonDel = await responseDel.Content.ReadAsStringAsync();

            //Console.WriteLine("Producto Eliminado:");
            //Console.WriteLine($"Id Eliminado: {id}");
            //Console.WriteLine($"Respuesta API: {jsonDel}\n");
        }
    }
}
