using JetKings.Data;
using JetKings.IService;
using JetKings.Models;
using Microsoft.EntityFrameworkCore;

namespace JetKings.Services;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default) =>
        await _dbSet.FirstOrDefaultAsync(u => u.Email == email.ToLower(), ct);

    public async Task<bool> EmailExistsAsync(string email, CancellationToken ct = default) =>
        await _dbSet.AnyAsync(u => u.Email == email.ToLower(), ct);
}
