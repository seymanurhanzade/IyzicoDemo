using IyzicoDemo.Entity;
using IyzicoDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace IyzicoDemo.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductByIdAsync(Guid id);
        Task<List<Product>> GettAllProductAsync();
        Task<List<Category>> GettAllProductToCategoryAsync();
        Task<List<Category>> GettAllCategoryAsync();
        Task<IActionResult> CreateProductAsync(CreateProductRequest request);
        void DeleteProductImage(Guid id);
        Task<IActionResult> CreateCategoryAsync(CreateCategoryRequest request);
        void DeleteCategory(Guid id);
        void DeleteProduct(Guid id);
        Task<IActionResult> UpdateProductAsync(Guid id, CreateProductRequest request);
        Task<IActionResult> UpdateCategoryAsync(Guid id, CreateCategoryRequest request);
    }
}
