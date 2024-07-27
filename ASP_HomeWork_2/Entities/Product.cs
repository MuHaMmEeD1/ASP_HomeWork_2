using System.ComponentModel.DataAnnotations;

namespace ASP_HomeWork_2.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Minimum value 3 simvol")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

    }
}
