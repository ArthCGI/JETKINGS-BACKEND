using JetKings.Entity;
using JetKings.IService;
using Microsoft.EntityFrameworkCore;

namespace JetKings.Services;

public class BuyerProductPriceRepository : IBuyerProductPriceRepository
{
    private readonly JetKingsDbContext _context;

    public BuyerProductPriceRepository(JetKingsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BuyerProductPrice>> GetAllAsync()
    {
        return await _context.BuyerProductPrices.ToListAsync();
    }

    public async Task<BuyerProductPrice?> GetByIdAsync(int id)
    {
        return await _context.BuyerProductPrices.FindAsync(id);
    }

    public async Task<BuyerProductPrice> CreateAsync(BuyerProductPrice entity)
    {
        _context.BuyerProductPrices.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(BuyerProductPrice entity)
    {
        _context.BuyerProductPrices.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(BuyerProductPrice entity)
    {
        _context.BuyerProductPrices.Remove(entity);
        await _context.SaveChangesAsync();
    }
}