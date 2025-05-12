namespace UniSystem.Core.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Declareation { get; set; }
        public bool IsSold { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public string PictureTxt { get; set; }

    }
}
