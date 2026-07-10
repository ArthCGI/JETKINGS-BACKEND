using JetKings.Application.Interfaces.IRepositories;
using JetKings.Domain.Entities;
using JetKings.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JetKings.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
        await _dbSet.FirstOrDefaultAsync(u => u.Email == email.ToLower(), cancellationToken);

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default) =>
        await _dbSet.AnyAsync(u => u.Email == email.ToLower(), cancellationToken);
}
