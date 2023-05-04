namespace Infastructure.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        //public AppUser Creator { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
