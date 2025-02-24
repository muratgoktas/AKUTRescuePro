using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using AutoMapper;


using AKUTRescue.Application.Features.Warehouses.Rules;

namespace AKUTRescue.Application.Features.Warehouses.Commands.CreateWarehouse
{
    public class CreateWarehouseCommand : IRequest<WarehouseResponseDto>
    {
        public CreateWarehouseRequestDto RequestDto { get; set; }

        public CreateWarehouseCommand(CreateWarehouseRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    }

    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, WarehouseResponseDto>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;
        private readonly CreateWarehouseCommandValidator _validator;

        public CreateWarehouseCommandHandler(
            IWarehouseRepository warehouseRepository,
            IMapper mapper,
            WarehouseBusinessRules warehouseBusinessRules)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
            _warehouseBusinessRules = warehouseBusinessRules;
            _validator = new CreateWarehouseCommandValidator();
        }

        public async Task<WarehouseResponseDto> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.RequestDto, cancellationToken);
            await _warehouseBusinessRules.WarehouseCodeCannotBeDuplicatedWhenInserted(request.RequestDto.Code);

            var warehouse = _mapper.Map<Warehouse>(request.RequestDto);
            await _warehouseRepository.AddAsync(warehouse);

            return _mapper.Map<WarehouseResponseDto>(warehouse);
        }
    }

    public class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseRequestDto>
    {
        public CreateWarehouseCommandValidator()
        {
            RuleFor(w => w.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(w => w.Code)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(w => w.Address)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(w => w.LocationId)
                .NotEmpty();

            RuleFor(w => w.ResponsibleMemberId)
                .NotEmpty();

            RuleFor(w => w.Type)
                .IsInEnum();

            RuleFor(w => w.Capacity)
                .GreaterThan(0);
        }
    }
} 