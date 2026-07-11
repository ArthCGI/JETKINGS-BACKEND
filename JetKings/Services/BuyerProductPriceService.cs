using AutoMapper;
using JetKings.Entity;
using JetKings.IService;
using JetKings.Models.DTOs;

namespace JetKings.Services;

public class BuyerProductPriceService : IBuyerProductPriceService
{
    private readonly IBuyerProductPriceRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<BuyerProductPriceService> _logger;

    public BuyerProductPriceService(
        IBuyerProductPriceRepository repository,
        IMapper mapper,
        ILogger<BuyerProductPriceService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResponseDto<PagedResponseDto<BuyerProductPriceResponseDto>>> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken ct = default)
    {
        var data = await _repository.GetAllAsync();

        var totalCount = data.Count();

        var pagedItems = data
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var result = new PagedResponseDto<BuyerProductPriceResponseDto>
        {
            Items = _mapper.Map<IEnumerable<BuyerProductPriceResponseDto>>(pagedItems),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };

        return ApiResponseDto<PagedResponseDto<BuyerProductPriceResponseDto>>.Ok(result);
    }

    public async Task<ApiResponseDto<BuyerProductPriceResponseDto>> GetByIdAsync(
        int id,
        CancellationToken ct = default)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return ApiResponseDto<BuyerProductPriceResponseDto>
                .Fail($"Record with Id {id} not found.");

        return ApiResponseDto<BuyerProductPriceResponseDto>
            .Ok(_mapper.Map<BuyerProductPriceResponseDto>(entity));
    }

    public async Task<ApiResponseDto<BuyerProductPriceResponseDto>> CreateAsync(
        CreateBuyerProductPriceRequestDto dto,
        CancellationToken ct = default)
    {
        var entity = _mapper.Map<BuyerProductPrice>(dto);

        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;

        var created = await _repository.CreateAsync(entity);

        return ApiResponseDto<BuyerProductPriceResponseDto>.Ok(
            _mapper.Map<BuyerProductPriceResponseDto>(created),
            "Buyer Product Price created successfully.");
    }

    public async Task<ApiResponseDto<BuyerProductPriceResponseDto>> UpdateAsync(
        int id,
        UpdateBuyerProductPriceRequestDto dto,
        CancellationToken ct = default)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return ApiResponseDto<BuyerProductPriceResponseDto>
                .Fail($"Record with Id {id} not found.");

        _mapper.Map(dto, entity);

        entity.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(entity);

        return ApiResponseDto<BuyerProductPriceResponseDto>.Ok(
            _mapper.Map<BuyerProductPriceResponseDto>(entity),
            "Updated successfully.");
    }

    public async Task<ApiResponseDto<bool>> DeleteAsync(
        int id,
        CancellationToken ct = default)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return ApiResponseDto<bool>.Fail($"Record with Id {id} not found.");

        await _repository.DeleteAsync(entity);

        return ApiResponseDto<bool>.Ok(
            true,
            "Deleted successfully.");
    }
}