using AKUTRescue.Core.Repositories.EfCore;
using AKUTRescue.Domain.Entities;
using AKUTRescue.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKUTRescue.Persistence.Repositories
{
    public class WarehouseRepository : EfRepositoryBase<Warehouse, AKUTRescueDbContext>, IWarehouseRepository
    {
        public WarehouseRepository(AKUTRescueDbContext context) : base(context)
        {
        }

        public async Task<int> GetTotalItemCount(Guid warehouseId)
        {
            return await Context.WarehouseItems
                .Where(wi => wi.WarehouseId == warehouseId)
                .SumAsync(wi => wi.Quantity);
        }

        public async Task<IList<Warehouse>> GetWarehousesByLocationAsync(Guid locationId)
        {
            return await Context.Warehouses
                .Include(w => w.Location)
                .Include(w => w.ResponsibleMember)
                .Where(w => w.LocationId == locationId)
                .ToListAsync();
        }

        public async Task<Warehouse> GetWithItemsAsync(Guid warehouseId)
        {
            return await Context.Warehouses
                .Include(w => w.Items)
                    .ThenInclude(wi => wi.Item)
                .Include(w => w.Location)
                .Include(w => w.ResponsibleMember)
                .FirstOrDefaultAsync(w => w.Id == warehouseId);
        }
    }
}