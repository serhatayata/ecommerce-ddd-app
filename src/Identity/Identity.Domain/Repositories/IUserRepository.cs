using Common.Domain.Repositories;
using Identity.Domain.Models;

namespace Identity.Domain.Repositories;

public interface IUserRepository : IRepository<ApplicationUser>
{
    // Query Methods
    Task<ApplicationUser?> FindByIdAsync(int id);
    Task<ApplicationUser?> FindByEmailAsync(string email);
    Task<ApplicationUser?> FindByUsernameAsync(string username);
    
    // Existence Checks
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByUsernameAsync(string username);
    
    // CRUD Operations
    Task<ApplicationUser> AddAsync(ApplicationUser user);
    Task UpdateAsync(ApplicationUser user);
    Task DeleteAsync(ApplicationUser user);
    
    // Role-related Queries
    Task<IReadOnlyList<ApplicationUser>> FindByRoleAsync(string roleName);
    Task<bool> IsInRoleAsync(int userId, string roleName);
    
    // Claims-related Queries
    Task<IReadOnlyList<ApplicationUser>> FindByClaimAsync(string claimType, string claimValue);
    
    // Pagination
    Task<IReadOnlyList<ApplicationUser>> GetPagedAsync(
        int pageNumber, 
        int pageSize, 
        string? searchTerm = null,
        string? orderBy = null);
    
    Task<int> GetTotalCountAsync(string? searchTerm = null);
}