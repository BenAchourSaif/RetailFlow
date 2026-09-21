using RetailFlow.Application.DTOs.Sales;
using RetailFlow.Application.Interfaces;
using RetailFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetailFlow.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;

        private readonly Dictionary<Guid, Sale> _sales = new();

        public SaleService(
            IProductService productService,
            IInventoryService inventoryService)
        {
            _productService = productService;
            _inventoryService = inventoryService;
        }

        public SaleResponse Create(CreateSaleRequest request)
        {
            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                StoreId = request.StoreId,
                CreatedAt = DateTime.UtcNow
            };

            foreach (var lineRequest in request.Lines)
            {
                var product = _productService.Get(lineRequest.ProductId);

                if (product is null)
                    throw new KeyNotFoundException("Product not found.");

                var inventory = _inventoryService.Get(
                    request.StoreId,
                    lineRequest.ProductId);

                if (inventory is null)
                    throw new KeyNotFoundException("Inventory not found.");

                var unitCost = inventory.AverageCost;

                _inventoryService.RemoveStock(
                    request.StoreId,
                    lineRequest.ProductId,
                    lineRequest.Quantity);

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

            _sales[sale.Id] = sale;

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
