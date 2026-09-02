namespace IyzicoDemo.Models
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = null!;
        public long Price { get; set; }
        public List<IFormFile>? ImageFile { get; set; }
        public string Explanation { get; set; } = null!;
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class CreateCategoryRequest
    {
        public string CategoryName { get; set; } = null!;
    }
}
