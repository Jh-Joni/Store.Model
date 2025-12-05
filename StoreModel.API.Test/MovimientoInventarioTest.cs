using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Store.Model;

namespace StoreModel.API.Test
{
    internal class MovimientoInventarioTest
    {
        private static readonly HttpClient httpClient =
            new HttpClient { BaseAddress = new Uri("https://store-model.onrender.com/api/") };


        public static async Task CrearActualizarEliminar(int productoId)
        {
            
            var chkProd = await httpClient.GetAsync($"Productos/{productoId}");
            if (!chkProd.IsSuccessStatusCode)
            {
                Console.WriteLine($"ERROR: El producto con Id {productoId} NO existe. Código {chkProd.StatusCode}");
                var txt = await chkProd.Content.ReadAsStringAsync();
                Console.WriteLine($"Respuesta API: {txt}");
                return;
            }

            
            // 1) CREAR MOVIMIENTO  (FECHA EN UTC)
           
            var nuevo = new MovimientoInventario
            {
                Tipo = "Entrada",
                Cantidad = 15,
                Fecha = DateTime.Parse("2025-12-04T15:30:00Z").ToUniversalTime(),  
                ProductoId = productoId
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(nuevo),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync("MovimientosInventario", content);
            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json) || json.TrimStart()[0] != '{')
            {
                Console.WriteLine("Error al crear movimiento. Respuesta no JSON:");
                Console.WriteLine(json);
                return;
            }

            MovimientoInventario creado;
            try
            {
                creado = JsonConvert.DeserializeObject<MovimientoInventario>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al deserializar la respuesta del POST:");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Respuesta cruda:");
                Console.WriteLine(json);
                return;
            }

            int id = creado?.Id ?? 0;

            Console.WriteLine("Movimiento Creado:");
            Console.WriteLine($"Id: {id}");
            Console.WriteLine($"Tipo: {creado?.Tipo}");
            Console.WriteLine($"Cantidad: {creado?.Cantidad}");
            Console.WriteLine($"Fecha: {creado?.Fecha}");
            Console.WriteLine($"ProductoId: {creado?.ProductoId}\n");

            if (id == 0)
            {
                Console.WriteLine("ERROR: La API no devolvió un ID valido.");
                return;
            }

          
            // 2) ACTUALIZAR MOVIMIENTO  
            
            var actualizado = new MovimientoInventario
            {
                Id = id,
                Tipo = "Salida",
                Cantidad = 3,
                Fecha = DateTime.Parse("2025-12-04T15:30:00Z").ToUniversalTime(), 
                ProductoId = productoId
            };

            content = new StringContent(
                JsonConvert.SerializeObject(actualizado),
                System.Text.Encoding.UTF8,
                "application/json");

            await httpClient.PutAsync($"MovimientosInventario/{id}", content);

            
            // 3) CONSULTAR MOVIMIENTO ACTUALIZADO 
            
            var responseGet = await httpClient.GetAsync($"MovimientosInventario/{id}");
            var jsonGet = await responseGet.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(jsonGet) || jsonGet.TrimStart()[0] != '{')
            {
                Console.WriteLine("Error al obtener el movimiento. Respuesta no JSON:");
                Console.WriteLine(jsonGet);
                return;
            }

            MovimientoInventario final;
            try
            {
                final = JsonConvert.DeserializeObject<MovimientoInventario>(jsonGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al deserializar el GET:");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Respuesta cruda:");
                Console.WriteLine(jsonGet);
                return;
            }

            Console.WriteLine("Movimiento Actualizado:");
            Console.WriteLine($"Id: {final?.Id}");
            Console.WriteLine($"Tipo: {final?.Tipo}");
            Console.WriteLine($"Cantidad: {final?.Cantidad}");
            Console.WriteLine($"Fecha: {final?.Fecha}");
            Console.WriteLine($"ProductoId: {final?.ProductoId}\n");

          
            // 4) ELIMINAR MOVIMIENTO (Opcional)
           
            //var responseDel = await httpClient.DeleteAsync($"MovimientosInventario/{id}");
            //var jsonDel = await responseDel.Content.ReadAsStringAsync();

            //Console.WriteLine("Movimiento Eliminado:");
            //Console.WriteLine($"Id Eliminado: {id}");
            //Console.WriteLine($"Respuesta API: {jsonDel}\n");
        }
    }
}
