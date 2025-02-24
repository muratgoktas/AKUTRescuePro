using MediatR;
using AKUTRescue.Application.Features.Teams.Rules;
using AKUTRescue.Application.Services.Repositories;
using AKUTRescue.Domain.Entities;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AKUTRescue.Application.Features.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommand : IRequest<DeletedTeamResponse>
    {
        public Guid Id { get; set; }
    }

    public class DeletedTeamResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }

    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, DeletedTeamResponse>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly TeamBusinessRules _teamBusinessRules;

        public DeleteTeamCommandHandler(
            ITeamRepository teamRepository,
            TeamBusinessRules teamBusinessRules)
        {
            _teamRepository = teamRepository;
            _teamBusinessRules = teamBusinessRules;
        }

        public async Task<DeletedTeamResponse> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            await _teamBusinessRules.TeamShouldExistWhenRequested(request.Id);
            await _teamBusinessRules.TeamShouldNotHaveActiveMembers(request.Id);

            var team = await _teamRepository.GetByIdAsync(request.Id);
            team.Status = false;
            team.DeleteDate = DateTime.UtcNow;

            await _teamRepository.UpdateAsync(team);

            return new DeletedTeamResponse
            {
                Id = request.Id,
                Message = Messages.Team.Deleted
            };
        }
    }

    public class DeleteTeamCommandValidator : AbstractValidator<DeleteTeamCommand>
    {
        public DeleteTeamCommandValidator()
        {
            RuleFor(t => t.Id)
                .NotEmpty()
                .WithMessage("Ekip ID'si boş olamaz.");
        }
    }
}