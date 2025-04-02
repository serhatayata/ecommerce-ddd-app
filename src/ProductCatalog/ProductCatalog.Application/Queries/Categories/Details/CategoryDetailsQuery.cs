using AutoMapper;
using MediatR;
using ProductCatalog.Application.Queries.Categories.Common;
using ProductCatalog.Domain.Contracts;

namespace ProductCatalog.Application.Queries.Categories.Details;

public class CategoryDetailsQuery : IRequest<CategoryResponse>
{
    public int Id { get; set; }

    public class CategoryDetailsQueryHandler : IRequestHandler<CategoryDetailsQuery, CategoryResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CategoryDetailsQueryHandler(
        IProductRepository productRepository, 
        IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CategoryResponse> Handle(
        CategoryDetailsQuery request, 
        CancellationToken cancellationToken)
        {
            var category = await _productRepository.GetCategoryByIdAsync(request.Id, cancellationToken);
            if (category == null) return null;

            var categoryResponse = _mapper.Map<CategoryResponse>(category);
            return categoryResponse;
        }
    }
}