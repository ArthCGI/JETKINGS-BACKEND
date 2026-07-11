using JetKings.Entity;

namespace JetKings.IService;

public interface IBuyerProductPriceRepository
{
    Task<IEnumerable<BuyerProductPrice>> GetAllAsync();
    Task<BuyerProductPrice?> GetByIdAsync(int id);
    Task<BuyerProductPrice> CreateAsync(BuyerProductPrice entity);
    Task UpdateAsync(BuyerProductPrice entity);
    Task DeleteAsync(BuyerProductPrice entity);
}