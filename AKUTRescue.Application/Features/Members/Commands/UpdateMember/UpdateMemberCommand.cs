using MediatR;
using AKUTRescue.Application.Features.Members.Rules;
using AKUTRescue.Application.Services.Repositories;
using AutoMapper;
using FluentValidation;


namespace AKUTRescue.Application.Features.Members.Commands.UpdateMember
{
    public class UpdateMemberCommand : IRequest<UpdatedMemberResponse>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid TeamId { get; set; }
        public Guid AuthorityId { get; set; }
        public bool Status { get; set; }
        public UpdateMemberRequestDto RequestDto { get; set; }
    }

    public class UpdatedMemberResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, UpdatedMemberResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly MemberBusinessRules _memberBusinessRules;
        private readonly UpdateMemberCommandValidator _validator;

        public UpdateMemberCommandHandler(
            IMemberRepository memberRepository,
            IMapper mapper,
            MemberBusinessRules memberBusinessRules)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
            _memberBusinessRules = memberBusinessRules;
            _validator = new UpdateMemberCommandValidator();
        }

        public async Task<UpdatedMemberResponse> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            await _memberBusinessRules.MemberShouldExistWhenRequested(request.Id);

            var member = await _memberRepository.GetByIdAsync(request.Id);
            _mapper.Map(request, member);
            member.UpdateDate = DateTime.UtcNow;

            await _memberRepository.UpdateAsync(member);

            return new UpdatedMemberResponse
            {
                Id = member.Id,
                FullName = $"{member.FirstName} {member.LastName}",
                Email = member.Email
            };
        }
    }

    public class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
    {
        public UpdateMemberCommandValidator()
        {
            RuleFor(m => m.Id)
                .NotEmpty();

            RuleFor(m => m.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);

            RuleFor(m => m.TeamId)
                .NotEmpty();

            RuleFor(m => m.AuthorityId)
                .NotEmpty();
        }
    }
}