using JetKings.Entity;
namespace JetKings.IService
{
    public interface IBuyerRepository
    {

        Task<IEnumerable<Buyer>> GetAllAsync();
        Task<Buyer?> GetByIdAsync(int id);
        Task<Buyer> CreateAsync(Buyer buyer);
        Task UpdateAsync(Buyer buyer);
        Task DeleteAsync(Buyer buyer);

    }
}
