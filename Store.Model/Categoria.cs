using System.ComponentModel.DataAnnotations;

namespace Store.Model
{
    public class Categoria
    {
        [Key]   
        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<Producto>? Productos { get; set; }

    }
}
