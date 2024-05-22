using Microsoft.EntityFrameworkCore;
using WebFruit.Data;
using WebFruit.DTOs;
using WebFruit.Interfaces;
using WebFruit.Models;

namespace WebFruit.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly FruitDbContext _context;
        public ProductRepository(FruitDbContext context)
        {
            _context = context;
        }

        #region Category
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }
        public async Task<Category> GetCategoryAsync(int id)
        {
            var query = _context.Categories.AsQueryable();
            return await query.FirstOrDefaultAsync(c=> c.CategoryId == id);
        }
        public async Task<bool> AddCategoryAsync(CategoryDTO categoryDTO)
        {
            var category = new Category
            {
                CategoryName = categoryDTO.CategoryName,
            };

            _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCategoryAsync(int id, CategoryDTO categoryDTO)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if(category == null)
                {
                    return false;
                }
                category.CategoryName = categoryDTO.CategoryName;
                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);

                if (category == null)
                    return false;

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion Category

        #region  Product
        public async Task<List<Product>> GetAllProductsAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            try
            {
                var query = _context.Products.AsQueryable();

                // Filtering
                if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
                {
                    if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query.Where(x => x.ProductName.Contains(filterQuery));
                    }
                }

                // Sorting
                if (!string.IsNullOrWhiteSpace(sortBy))
                {
                    if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    {
                        query = isAscending ? query.OrderBy(x => x.ProductName) : query.OrderByDescending(x => x.ProductName);
                    }
                }

                // Pagination
                var skipResults = (pageNumber - 1) * pageSize;
                var paginatedResults = await query.Skip(skipResults).Take(pageSize).ToListAsync();

                return paginatedResults;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Product> GetProductAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }
        public async Task<bool> AddProductAsync(ProductDTO productDto)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == productDto.CategoryId);
            var product = new Product
            {
                ProductName = productDto.ProductName,
                ProductDescription = productDto.ProductDescription,
                ImageUrl = productDto.ImageUrl,
                Quantity = productDto.Quantity,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId
            };

            await _context.Products.AddAsync(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProductAsync(int id, ProductDTO productDto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
                return false;

            product.ProductName = productDto.ProductName;
            product.ProductDescription = productDto.ProductDescription;
            product.ImageUrl = productDto.ImageUrl;
            product.Quantity = productDto.Quantity;
            product.Price = productDto.Price;
            product.CategoryId = productDto.CategoryId;

            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }
        #endregion Product
    }
}
