using System.ComponentModel.DataAnnotations;

namespace Infastructure.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public string ImageSource { get; set; }
        public int SoldCounter { get; set; }
    }
}
