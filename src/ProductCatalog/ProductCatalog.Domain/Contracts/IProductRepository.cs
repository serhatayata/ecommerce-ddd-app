using Common.Domain.Repositories;
using ProductCatalog.Domain.Models.Products;

namespace ProductCatalog.Domain.Contracts;

public interface IProductRepository : IRepository<Product>
{

}