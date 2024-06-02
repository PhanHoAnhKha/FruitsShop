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
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
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
                CategoryId = productDto.CategoryId,
                IsSaling = productDto.IsSaling
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
            product.IsSaling = productDto.IsSaling;

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

        public async Task<bool> AddEmailSubscription(EmailSubscribeDTO emailDto)
        {
            try
            {
                var existingEmail = await _context.EmailSubscriptions.FirstOrDefaultAsync(e => e.Email == emailDto.Email);
                if (existingEmail != null)
                {
                    return true;
                }

                var newEmail = new EmailSubscriptions
                {
                    Email = emailDto.Email
                };
                _context.EmailSubscriptions.Add(newEmail);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Product> SearchProductsByName(string productName)
        {
            return _context.Products.Where(p => EF.Functions.Like(p.ProductName, $"%{productName}%")).ToList();
        }
        #endregion Product

        #region Recommend
        public async Task<List<SearchHistory>> GetSearchHistoryByUserIdAsync(int userId)
        {
            return await _context.SearchHistories.Where(sh => sh.UserId == userId).ToListAsync();
        }

        public async Task<List<PurchaseHistory>> GetPurchaseHistoryByUserIdAsync(int userId)
        {
            return await _context.PurchaseHistories.Where(ph => ph.UserId == userId).ToListAsync();
        }

		public async Task<List<Product>> GetRecommendedProductsAsync(int userId)
		{
			try
			{
				var searchHistory = await GetSearchHistoryByUserIdAsync(userId);

				var recommendedProducts = new List<Product>();
				foreach (var term in searchHistory.Select(sh => sh.SearchTerm).Distinct())
				{
					var productsRelatedToTerm = _context.Products
						.Where(p => p.ProductName.Contains(term))
						.ToList();

					recommendedProducts.AddRange(productsRelatedToTerm);
				}

				return recommendedProducts;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<bool> AddSearchHistory(int userId, string searchTerm)
		{
			try
			{
				var searchHistory = new SearchHistory
				{
					UserId = userId,
					SearchTerm = searchTerm,
				};

				_context.SearchHistories.Add(searchHistory);

				await _context.SaveChangesAsync();


				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		#endregion Recommend
	}
}
