namespace UniSystem.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Declareation { get; set; }
        public DateTime AddingDate { get; set; }
        public DateTime? SoldDate { get; set; }
        public bool IsActive { get; set; }
        public bool? IsSold { get; set; }
        public decimal Price { get; set; }
        public string PictureTxt { get; set; }
        public int Stock { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
