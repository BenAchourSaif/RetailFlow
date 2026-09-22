using RetailFlow.Application.DTOs.Sales;
using RetailFlow.Application.Interfaces;
using RetailFlow.Domain.Entities;
using RetailFlow.Infrastructure.Persistence;

namespace RetailFlow.Infrastructure.Services
{
    public class SaleService : ISaleService
    {
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;
        private readonly ISaleRepository _saleRepository;
        private readonly RetailFlowDbContext _db;

        public SaleService(
            IProductService productService,
            IInventoryService inventoryService,
            ISaleRepository saleRepository)
        {
            _productService = productService;
            _inventoryService = inventoryService;
            _saleRepository = saleRepository;
        }
       
        public async Task<SaleResponse> CreateAsync(
    CreateSaleRequest request,
    CancellationToken cancellationToken = default)
        {
            var storeExists = _db.Stores.Any(x => x.Id == request.StoreId);

            if (!storeExists)
                throw new KeyNotFoundException("Store not found.");

            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                StoreId = request.StoreId,
                CreatedAt = DateTime.UtcNow
            };

            foreach (var lineRequest in request.Lines)
            {
                var product = await _productService.GetAsync(
                                lineRequest.ProductId,
                                cancellationToken);

                if (product is null)
                    throw new KeyNotFoundException("Product not found.");

                var inventory = await _inventoryService.GetAsync(
                         request.StoreId,
                         lineRequest.ProductId,
                         cancellationToken);

                if (inventory is null)
                    throw new KeyNotFoundException("Inventory not found.");

                var unitCost = inventory.AverageCost;

                await _inventoryService.RemoveStockAsync(
                     request.StoreId,
                     lineRequest.ProductId,
                     lineRequest.Quantity,
                     cancellationToken);

                var line = new SaleLine
                {
                    Id = Guid.NewGuid(),
                    SaleId = sale.Id,
                    ProductId = product.Id,
                    Quantity = lineRequest.Quantity,
                    UnitPrice = product.SellingPrice,
                    UnitCost = unitCost,
                    VatRate = product.VatRate
                };

                sale.Lines.Add(line);

                sale.TotalAmount +=
                    line.Quantity * line.UnitPrice;

                sale.TotalCost +=
                    line.Quantity * line.UnitCost;
            }
            await _saleRepository.AddAsync(
                               sale,
                               cancellationToken);

            await _saleRepository.SaveChangesAsync(
                cancellationToken);

            return new SaleResponse
            {
                Id = sale.Id,
                StoreId = sale.StoreId,
                TotalAmount = sale.TotalAmount,
                TotalCost = sale.TotalCost,
                Margin = sale.TotalAmount - sale.TotalCost
            };
        }
    }
}
