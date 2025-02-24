using MediatR;
using AKUTRescue.Application.Features.Members.Rules;
using AKUTRescue.Application.Services.Repositories;
using FluentValidation;
using AKUTRescue.Domain.Entities;
using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AKUTRescue.Application.Features.Members.Commands.DeleteMember
{
    public class DeleteMemberCommand : IRequest<DeletedMemberResponse>
    {
        public Guid Id { get; set; }
    }

    public class DeletedMemberResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }

    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, DeletedMemberResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly MemberBusinessRules _memberBusinessRules;

        public DeleteMemberCommandHandler(
            IMemberRepository memberRepository,
            MemberBusinessRules memberBusinessRules)
        {
            _memberRepository = memberRepository;
            _memberBusinessRules = memberBusinessRules;
        }

        public async Task<DeletedMemberResponse> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            await _memberBusinessRules.MemberShouldExistWhenRequested(request.Id);

            var member = await _memberRepository.GetByIdAsync(request.Id);
            
            // Soft delete
            member.Status = false;
            member.DeleteDate = DateTime.UtcNow;
            
            await _memberRepository.UpdateAsync(member);

            return new DeletedMemberResponse
            {
                Id = request.Id,
                Message = Messages.Member.Deleted
            };
        }
    }

    public class DeleteMemberCommandValidator : AbstractValidator<DeleteMemberCommand>
    {
        public DeleteMemberCommandValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty()
                .WithMessage("Üye ID'si boş olamaz.");
        }
    }
}