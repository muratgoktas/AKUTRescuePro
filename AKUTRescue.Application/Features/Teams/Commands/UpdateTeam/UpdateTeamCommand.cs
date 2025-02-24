using System;
using System.Threading;
using System.Threading.Tasks;
using AKUTRescue.Application.Features.Teams.Rules;
using AKUTRescue.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using AKUTRescue.Application.Services.Repositories;
using AutoMapper;
using FluentValidation;

namespace AKUTRescue.Application.Features.Teams.Commands.UpdateTeam
{
    public class UpdateTeamCommand : IRequest<UpdatedTeamResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public TeamType Type { get; set; }
        public Guid LocationId { get; set; }
        public Guid TeamLeaderId { get; set; }
        public Guid? ParentTeamId { get; set; }
        public bool Status { get; set; }
    }

    public class UpdatedTeamResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, UpdatedTeamResponse>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly TeamBusinessRules _teamBusinessRules;
        private readonly UpdateTeamCommandValidator _validator;

        public UpdateTeamCommandHandler(
            ITeamRepository teamRepository,
            IMapper mapper,
            TeamBusinessRules teamBusinessRules)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
            _teamBusinessRules = teamBusinessRules;
            _validator = new UpdateTeamCommandValidator();
        }

        public async Task<UpdatedTeamResponse> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            await _teamBusinessRules.TeamShouldExistWhenRequested(request.Id);

            var team = await _teamRepository.GetByIdAsync(request.Id);
            if (team.Code != request.Code)
                await _teamBusinessRules.TeamCodeCannotBeDuplicatedWhenInserted(request.Code);

            _mapper.Map(request, team);
            team.UpdateDate = DateTime.UtcNow;

            await _teamRepository.UpdateAsync(team);

            return new UpdatedTeamResponse
            {
                Id = team.Id,
                Name = team.Name,
                Code = team.Code
            };
        }
    }

    public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
    {
        public UpdateTeamCommandValidator()
        {
            RuleFor(t => t.Id)
                .NotEmpty();

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