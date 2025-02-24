

namespace AKUTRescue.Application.Features.Warehouses.Rules
{
    public class WarehouseBusinessRules
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseBusinessRules(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task WarehouseCodeCannotBeDuplicatedWhenInserted(string code)
        {
            var result = await _warehouseRepository.AnyAsync(w => w.Code == code);
            if (result)
                throw new BusinessException("Bu kod ile kayıtlı depo bulunmaktadır.");
        }

        public async Task WarehouseShouldExistWhenRequested(Guid id)
        {
            var result = await _warehouseRepository.GetByIdAsync(id);
            if (result == null)
                throw new BusinessException("Depo bulunamadı.");
        }

        public async Task WarehouseShouldHaveCapacityForNewItems(Guid warehouseId, int requestedQuantity)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId);
            var currentItems = await _warehouseRepository.GetTotalItemCount(warehouseId);
            
            if (warehouse.Capacity < (currentItems + requestedQuantity))
                throw new BusinessException("Depoda yeterli kapasite bulunmamaktadır.");
        }
    }
} 