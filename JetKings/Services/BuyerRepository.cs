using JetKings.Entity;
using JetKings.IService;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace JetKings.Services;

public class BuyerRepository : IBuyerRepository
{
    private readonly JetKingsDbContext _context;

    public BuyerRepository(JetKingsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Buyer>> GetAllAsync()
    {
        return await _context.Buyers.ToListAsync();
    }

    public async Task<Buyer?> GetByIdAsync(int id)
    {
        return await _context.Buyers.FindAsync(id);
    }

    public async Task<Buyer> CreateAsync(Buyer buyer)
    {
        _context.Buyers.Add(buyer);
        await _context.SaveChangesAsync();
        return buyer;
    }

    public async Task UpdateAsync(Buyer buyer)
    {
        _context.Buyers.Update(buyer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Buyer buyer)
    {

        // Delete buyer product prices
        var prices = await _context.BuyerProductPrices
            .Where(x => x.BuyerId == buyer.Id)
            .ToListAsync();

        _context.BuyerProductPrices.RemoveRange(prices);

        // Get buyer invoices
        var invoices = await _context.Invoices
            .Where(x => x.BuyerId == buyer.Id)
            .ToListAsync();

        // Delete invoice items first
        foreach (var invoice in invoices)
        {
            var items = await _context.InvoiceItems
                .Where(x => x.InvoiceId == invoice.Id)
                .ToListAsync();

            _context.InvoiceItems.RemoveRange(items);
        }

        // Delete invoices
        _context.Invoices.RemoveRange(invoices);

        // Delete buyer
        _context.Buyers.Remove(buyer);

        await _context.SaveChangesAsync();

    }
}
