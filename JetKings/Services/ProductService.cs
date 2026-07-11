using AutoMapper;
using JetKings.Entity;
using JetKings.IService;
using JetKings.Models.DTOs;
using Microsoft.Extensions.Logging;

namespace JetKings.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResponseDto<ProductResponseDto>> GetByIdAsync(
        int id,
        CancellationToken ct = default)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            return ApiResponseDto<ProductResponseDto>.Fail($"Product with Id {id} not found.");

        return ApiResponseDto<ProductResponseDto>.Ok(
            _mapper.Map<ProductResponseDto>(product));
    }

    public async Task<ApiResponseDto<PagedResponseDto<ProductResponseDto>>> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken ct = default)
    {
        var products = await _productRepository.GetAllAsync();

        var totalCount = products.Count();

        var pagedItems = products
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var result = new PagedResponseDto<ProductResponseDto>
        {
            Items = _mapper.Map<IEnumerable<ProductResponseDto>>(pagedItems),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };

        return ApiResponseDto<PagedResponseDto<ProductResponseDto>>.Ok(result);
    }

    public async Task<ApiResponseDto<ProductResponseDto>> CreateAsync(
        CreateProductRequestDto dto,
        CancellationToken ct = default)
    {
        var product = _mapper.Map<Product>(dto);

        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;

        var created = await _productRepository.CreateAsync(product);

        _logger.LogInformation("Product {ProductId} created", created.Id);

        return ApiResponseDto<ProductResponseDto>.Ok(
            _mapper.Map<ProductResponseDto>(created),
            "Product created successfully.");
    }

    public async Task<ApiResponseDto<ProductResponseDto>> UpdateAsync(
        int id,
        UpdateProductRequestDto dto,
        CancellationToken ct = default)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            return ApiResponseDto<ProductResponseDto>.Fail($"Product with Id {id} not found.");

        _mapper.Map(dto, product);

        product.UpdatedAt = DateTime.UtcNow;

        await _productRepository.UpdateAsync(product);

        return ApiResponseDto<ProductResponseDto>.Ok(
            _mapper.Map<ProductResponseDto>(product),
            "Product updated successfully.");
    }

    public async Task<ApiResponseDto<bool>> DeleteAsync(
        int id,
        CancellationToken ct = default)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            return ApiResponseDto<bool>.Fail($"Product with Id {id} not found.");

        await _productRepository.DeleteAsync(product);

        return ApiResponseDto<bool>.Ok(true, "Product deleted successfully.");
    }
}