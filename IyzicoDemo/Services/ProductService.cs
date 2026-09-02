using IyzicoDemo.Entity;
using IyzicoDemo.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IyzicoDemo.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _dbContext;
        public ProductService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetProductByIdAsync(Guid id)
        {
            try
            {
                var entities = await _dbContext.Products.Where(p => p.Id == id).Select(c => new Product
                {
                    Id = c.Id,
                    ProductName = c.ProductName,
                    Price = c.Price,
                    Explanation = c.Explanation,
                    Quantity = c.Quantity,
                    CategoryId = c.CategoryId,
                    ProductImages = c.ProductImages.Select(p => new ProductImage
                    {
                        Id = p.Id,
                        ImageUrl = p.ImageUrl,
                        ProductId = c.Id
                    }).ToList()
                }).ToListAsync();

                return entities;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> CreateCategoryAsync(CreateCategoryRequest request)
        {
            try
            {
                var entity = new Category
                {
                    CategoryName = request.CategoryName
                };
                await _dbContext.Categories.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return await Task.FromResult<IActionResult>(new OkObjectResult("Kategori oluşturuldu."));
            }
            catch (Exception ex)
            {
                return await Task.FromResult<IActionResult>(new BadRequestObjectResult(ex.Message));
            }
        }

        public void DeleteCategory(Guid id)
        {
            var entity = _dbContext.Categories.Find(id);
            if (entity != null)
            {
                _dbContext.Categories.Remove(entity);
                _dbContext.SaveChanges();
            }

        }

        public async Task<IActionResult> CreateProductAsync(CreateProductRequest request)
        {

            try
            {
                if (request.ImageFile == null || request.ImageFile.Count == 0)
                {
                    return new BadRequestObjectResult("En az bir resim dosyası yüklenmelidir.");
                }

                if (request.ImageFile.Count > 7)
                {
                    return new BadRequestObjectResult("En fazla 7 resim dosyası yüklenebilir.");
                }
                var entity = new Product
                {
                    ProductName = request.Name,
                    Price = request.Price,
                    Explanation = request.Explanation,
                    Quantity = request.Quantity,
                    CategoryId = request.CategoryId,
                    OfferActive = OfferActive.Inactive,
                    OfferPrice = 0
                };

                await _dbContext.Products.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                AddProductImageAsync(request.ImageFile, entity.Id).Wait();
                return new OkObjectResult("Ürün oluşturuldu." + entity);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public void DeleteProduct(Guid id)
        {
            var entity = _dbContext.Products.Find(id);
            if (entity != null)
            {
                _dbContext.Products.Remove(entity);
                _dbContext.SaveChanges();
            }

        }

        public async Task<IActionResult> AddProductImageAsync(List<IFormFile> imageFiles, Guid id)
        {
            var imageEntity = new List<ProductImage>();

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");


            foreach (var imageFile in imageFiles)
            {
                if (imageFile.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    string filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    imageEntity.Add(new ProductImage
                    {
                        ImageUrl = "images/" + fileName,
                        ProductId = id
                    });
                }
            }
            await _dbContext.ProductImages.AddRangeAsync(imageEntity);
            await _dbContext.SaveChangesAsync();
            return new OkObjectResult("Ürün resmi eklendi.");
        }
        public async Task<IActionResult> UpdateProductAsync(Guid id, CreateProductRequest request)
        {
            try
            {
                var product = await _dbContext.Products
                    .Include(x => x.ProductImages)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                {
                    return new BadRequestObjectResult($"Could not find product {id}");
                }

                product.ProductName = request.Name;
                product.Price = request.Price;
                product.Explanation = request.Explanation;
                product.Quantity = request.Quantity;
                product.CategoryId = request.CategoryId;

                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync();

                if (request.ImageFile != null && request.ImageFile.Count > 0)
                {
                    await AddProductImageAsync(request.ImageFile, id);
                }
                return new OkObjectResult("Ürün güncellendi.");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

        }
        public void DeleteProductImage(Guid id)
        {
            //var productImages= await _dbContext.Products.Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.Id == id);

            var productImg = _dbContext.ProductImages.Find(id);
            if (productImg != null)
            {
                _dbContext.ProductImages.Remove(productImg);
                _dbContext.SaveChangesAsync();

            }
            else
            {
                Console.WriteLine($"Could not find product image {id}");
            }
        }
        public async Task<IActionResult> UpdateCategoryAsync(Guid id, CreateCategoryRequest request)
        {
            var categoryId = await _dbContext.Categories.FindAsync(id);
            if (categoryId != null)
            {
                try
                {
                    categoryId.CategoryName = request.CategoryName;
                    await _dbContext.SaveChangesAsync();
                    return new OkObjectResult(new { message = "Başarılı." });
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult(new { message = "Başarısız güncelleme.", detail = ex.Message });

                }
            }
            else
            {
                return new BadRequestObjectResult($"Could not find category {id}");
            }
        }
        public async Task<List<Product>> GettAllProductAsync()
        {
            try
            {
                var entities = await _dbContext.Products.Select(c => new Product
                {
                    Id = c.Id,
                    ProductName = c.ProductName,
                    Price = c.Price,
                    Explanation = c.Explanation,
                    Quantity = c.Quantity,
                    CategoryId = c.CategoryId,
                    ProductImages = c.ProductImages.Select(p => new ProductImage
                    {
                        Id = p.Id,
                        ImageUrl = p.ImageUrl,
                        ProductId = c.Id
                    }).ToList()
                }).ToListAsync();

                return entities;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<Category>> GettAllCategoryAsync()
        {
            try
            {
                var entities = await _dbContext.Categories.Select(c => new Category
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName
                }).ToListAsync();

                return entities;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<Category>> GettAllProductToCategoryAsync()
        {
            try
            {
                var entities = await _dbContext.Categories.Include(c => c.Products).Select(x => new Category
                {
                    CategoryName = x.CategoryName,
                    Products = x.Products.Select(p => new Product
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        Price = p.Price
                    }).ToList()
                }).ToListAsync();

                return entities;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}
