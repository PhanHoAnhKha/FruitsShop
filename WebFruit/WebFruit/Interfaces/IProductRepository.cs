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
        Task<List<Product>> GetAllProductsAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100);
        Task<Product> GetProductAsync(int id);
        Task<bool> AddProductAsync(ProductDTO productDTO);

        Task<bool> UpdateProductAsync(int id, ProductDTO productDTO);

        Task<bool> DeleteProductAsync(int id);

    }
}
