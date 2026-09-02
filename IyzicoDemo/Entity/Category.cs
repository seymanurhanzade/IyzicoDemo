using System.ComponentModel.DataAnnotations;

namespace IyzicoDemo.Entity
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public List<Product> Products { get; set; } = null!;
    }
}
