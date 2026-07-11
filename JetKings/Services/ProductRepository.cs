using System.Diagnostics;
using JetKings.Entity;
using JetKings.IService;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace JetKings.Services;

public class ProductRepository : IProductRepository
{
    private readonly JetKingsDbContext _context;

    public ProductRepository(JetKingsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        // Delete buyer product prices
        var prices = await _context.BuyerProductPrices
            .Where(x => x.ProductId == product.Id)
            .ToListAsync();

        _context.BuyerProductPrices.RemoveRange(prices);

        // Delete invoice items
        var invoiceItems = await _context.InvoiceItems
            .Where(x => x.ProductId == product.Id)
            .ToListAsync();

        _context.InvoiceItems.RemoveRange(invoiceItems);

        // Delete product
        _context.Products.Remove(product);

        await _context.SaveChangesAsync();
    }
}