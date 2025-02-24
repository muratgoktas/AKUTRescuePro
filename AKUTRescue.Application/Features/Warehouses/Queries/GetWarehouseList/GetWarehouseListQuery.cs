using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;


namespace AKUTRescue.Application.Features.Warehouses.Queries.GetWarehouseList
{
    public class GetWarehouseListQuery : IRequest<List<WarehouseResponseDto>>
    {
        public bool? IsActive { get; set; }
        public WarehouseType? Type { get; set; }
        public Guid? LocationId { get; set; }
    }

    public class GetWarehouseListQueryHandler : IRequestHandler<GetWarehouseListQuery, List<WarehouseResponseDto>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetWarehouseListQueryHandler(IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<List<WarehouseResponseDto>> Handle(GetWarehouseListQuery request, CancellationToken cancellationToken)
        {
            var warehouses = await _warehouseRepository.GetListAsync(
                predicate: w => (!request.IsActive.HasValue || w.Status == request.IsActive.Value) &&
                               (!request.Type.HasValue || w.Type == request.Type.Value) &&
                               (!request.LocationId.HasValue || w.LocationId == request.LocationId.Value),
                include: q => q
                    .Include(w => w.Location)
                    .Include(w => w.ResponsibleMember)
                    .Include(w => w.Items),
                cancellationToken: cancellationToken
            );

            return _mapper.Map<List<WarehouseResponseDto>>(warehouses.Items);
        }
    }
} 