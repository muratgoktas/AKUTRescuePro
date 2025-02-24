using MediatR;
using AKUTRescue.Application.Features.Teams.Rules;
using AKUTRescue.Application.Services.Repositories;
using AutoMapper;
using FluentValidation;
using AKUTRescue.Domain.Entities;


namespace AKUTRescue.Application.Features.Teams.Commands.CreateTeam
{
    public class CreateTeamCommand : IRequest<TeamResponseDto>
    {
        public CreateTeamRequestDto RequestDto { get; set; }

        public CreateTeamCommand(CreateTeamRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    }

    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, TeamResponseDto>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly TeamBusinessRules _teamBusinessRules;
        private readonly CreateTeamCommandValidator _validator;

        public CreateTeamCommandHandler(
            ITeamRepository teamRepository,
            IMapper mapper,
            TeamBusinessRules teamBusinessRules)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
            _teamBusinessRules = teamBusinessRules;
            _validator = new CreateTeamCommandValidator();
        }

        public async Task<TeamResponseDto> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.RequestDto, cancellationToken);
            await _teamBusinessRules.TeamCodeCannotBeDuplicatedWhenInserted(request.RequestDto.Code);

            var team = _mapper.Map<Team>(request.RequestDto);
            await _teamRepository.AddAsync(team);

            return _mapper.Map<TeamResponseDto>(team);
        }
    }

    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamRequestDto>
    {
        public CreateTeamCommandValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(t => t.Code)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(t => t.Type)
                .IsInEnum();

            RuleFor(t => t.LocationId)
                .NotEmpty();

            RuleFor(t => t.TeamLeaderId)
                .NotEmpty();
        }
    }
}