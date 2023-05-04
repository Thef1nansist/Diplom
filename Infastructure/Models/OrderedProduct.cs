namespace Infastructure.Models
{
    public class OrderedProduct
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual Product Product { get; set; }
    }
}
