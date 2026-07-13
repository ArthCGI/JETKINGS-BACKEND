using JetKings.Entity;

namespace JetKings.IService;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
}