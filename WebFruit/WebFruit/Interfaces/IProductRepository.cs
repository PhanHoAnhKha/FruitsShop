using WebFruit.Models;
using WebFruit.DTOs;

namespace WebFruit.Interfaces
{
    public interface IProductRepository
    {
        // Categories
        Task<List<Category>> GetAllCategoriesAsync ();  
        Task<Category> GetCategoryAsync (int id);
        Task<bool> AddCategoryAsync(CategoryDTO categoryDTO);

        Task<bool> UpdateCategoryAsync(int id, CategoryDTO categoryDTO);

        Task<bool> DeleteCategoryAsync(int id);

        //Products
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductAsync(int id);
        Task<bool> AddProductAsync(ProductDTO productDTO);

        Task<bool> UpdateProductAsync(int id, ProductDTO productDTO);

        Task<bool> DeleteProductAsync(int id);
        Task<bool> AddEmailSubscription(EmailSubscribeDTO email);
        IEnumerable<Product> SearchProductsByName(string productName);

        Task<List<SearchHistory>> GetSearchHistoryByUserIdAsync(int userId);
        Task<List<PurchaseHistory>> GetPurchaseHistoryByUserIdAsync(int userId);
        Task<List<Product>> GetRecommendedProductsAsync(int userId);
		Task<bool> AddSearchHistory(int userId, string searchTerm);


	}
}
