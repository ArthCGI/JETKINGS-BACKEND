using AutoMapper;
using JetKings.Entity;
using JetKings.IService;
using JetKings.Models.DTOs;
using Microsoft.Extensions.Logging;

namespace JetKings.Services;

public class BuyerService : IBuyerService
{
    private readonly IBuyerRepository _buyerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<BuyerService> _logger;

    public BuyerService(
        IBuyerRepository buyerRepository,
        IMapper mapper,
        ILogger<BuyerService> logger)
    {
        _buyerRepository = buyerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResponseDto<BuyerResponseDto>> GetByIdAsync(
        int id,
        CancellationToken ct = default)
    {
        var buyer = await _buyerRepository.GetByIdAsync(id);

        if (buyer is null)
            return ApiResponseDto<BuyerResponseDto>.Fail(
                $"Buyer with ID {id} not found.");

        return ApiResponseDto<BuyerResponseDto>.Ok(
            _mapper.Map<BuyerResponseDto>(buyer));
    }

    public async Task<ApiResponseDto<PagedResponseDto<BuyerResponseDto>>> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken ct = default)
    {
        var buyers = await _buyerRepository.GetAllAsync();

        var totalCount = buyers.Count();

        var pagedBuyers = buyers
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var result = new PagedResponseDto<BuyerResponseDto>
        {
            Items = _mapper.Map<IEnumerable<BuyerResponseDto>>(pagedBuyers),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };

        return ApiResponseDto<PagedResponseDto<BuyerResponseDto>>.Ok(result);
    }

    public async Task<ApiResponseDto<BuyerResponseDto>> CreateAsync(
        CreateBuyerRequestDto dto,
        CancellationToken ct = default)
    {
        var buyer = _mapper.Map<Buyer>(dto);

        buyer.CreatedAt = DateTime.UtcNow;
        buyer.UpdatedAt = DateTime.UtcNow;

        var created = await _buyerRepository.CreateAsync(buyer);

        _logger.LogInformation("Buyer {BuyerId} created", created.Id);

        return ApiResponseDto<BuyerResponseDto>.Ok(
            _mapper.Map<BuyerResponseDto>(created),
            "Buyer created successfully.");
    }

    public async Task<ApiResponseDto<BuyerResponseDto>> UpdateAsync(
        int id,
        UpdateBuyerRequestDto dto,
        CancellationToken ct = default)
    {
        var buyer = await _buyerRepository.GetByIdAsync(id);

        if (buyer is null)
            return ApiResponseDto<BuyerResponseDto>.Fail(
                $"Buyer with ID {id} not found.");

        _mapper.Map(dto, buyer);

        buyer.UpdatedAt = DateTime.UtcNow;

        await _buyerRepository.UpdateAsync(buyer);

        _logger.LogInformation("Buyer {BuyerId} updated", id);

        return ApiResponseDto<BuyerResponseDto>.Ok(
            _mapper.Map<BuyerResponseDto>(buyer),
            "Buyer updated successfully.");
    }

    public async Task<ApiResponseDto<bool>> DeleteAsync(
        int id,
        CancellationToken ct = default)
    {
        var buyer = await _buyerRepository.GetByIdAsync(id);

        if (buyer is null)
            return ApiResponseDto<bool>.Fail(
                $"Buyer with ID {id} not found.");

        await _buyerRepository.DeleteAsync(buyer);

        _logger.LogInformation("Buyer {BuyerId} deleted", id);

        return ApiResponseDto<bool>.Ok(
            true,
            "Buyer deleted successfully.");
    }
}