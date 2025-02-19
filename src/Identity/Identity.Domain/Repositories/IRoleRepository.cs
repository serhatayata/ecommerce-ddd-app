using Identity.Domain.Models;

namespace Identity.Domain.Repositories;

public interface IRoleRepository
{
    // Query Methods
    Task<ApplicationRole?> FindByIdAsync(int id);
    Task<ApplicationRole?> FindByNameAsync(string roleName);
    
    // Existence Checks
    Task<bool> ExistsByNameAsync(string roleName);
    
    // CRUD Operations
    Task AddAsync(ApplicationRole role);
    Task UpdateAsync(ApplicationRole role);
    Task DeleteAsync(ApplicationRole role);
    
    // Claims-related Queries
    Task<IReadOnlyList<ApplicationRole>> FindByClaimAsync(string claimType, string claimValue);
    Task<bool> HasClaimAsync(int roleId, string claimType, string claimValue);
    Task<IReadOnlyList<ApplicationRoleClaim>> GetClaimsByRoleIdAsync(int roleId);
    
    // Role Management
    Task AddClaimAsync(int roleId, string claimType, string claimValue);
    Task RemoveClaimAsync(int roleId, string claimType, string claimValue);
    Task UpdateRoleNameAsync(int roleId, string newName);
    
    // Pagination and Listing
    Task<IReadOnlyList<ApplicationRole>> GetAllAsync();
    Task<IReadOnlyList<ApplicationRole>> GetPagedAsync(
        int pageNumber, 
        int pageSize, 
        string? searchTerm = null);
    
    Task<int> GetTotalCountAsync(string? searchTerm = null);
}