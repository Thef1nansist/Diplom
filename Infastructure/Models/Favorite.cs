using System.ComponentModel.DataAnnotations.Schema;

namespace Infastructure.Models
{
    public class Favorite
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        //public AppUser User { get; set; }
        public int CompanyId { get; set; }
        //public Company Company { get; set; }
    }
}
