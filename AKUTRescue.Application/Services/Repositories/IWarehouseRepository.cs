using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKUTRescue.Application.Services.Repositories;
using AKUTRescue.Domain.Entities;

public interface IWarehouseRepository : IAsyncRepository<Warehouse, Guid>
{
    Task<int> GetTotalItemCount(Guid warehouseId);
    Task<IList<Warehouse>> GetWarehousesByLocationAsync(Guid locationId);
    Task<Warehouse> GetWithItemsAsync(Guid warehouseId);
} 