using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AKUTRescue.Application.Features.Members.Commands.CreateMember;
using AKUTRescue.Domain.Entities;
using AKUTRescue.Application.Services.Repositories;
using FluentValidation;
using AKUTRescue.Application.Features.Members.Rules;


namespace AKUTRescue.Application.Features.Members.Commands.CreateMember
{
    public class CreateMemberCommand : IRequest<MemberResponseDto>
    {
        public CreateMemberRequestDto RequestDto { get; set; }

        public CreateMemberCommand(CreateMemberRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    }

    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, MemberResponseDto>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly MemberBusinessRules _memberBusinessRules;
        private readonly CreateMemberCommandValidator _validator;

        public CreateMemberCommandHandler(
            IMemberRepository memberRepository,
            IMapper mapper,
            MemberBusinessRules memberBusinessRules)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
            _memberBusinessRules = memberBusinessRules;
            _validator = new CreateMemberCommandValidator();
        }

        public async Task<MemberResponseDto> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.RequestDto, cancellationToken);
            await _memberBusinessRules.EmailCannotBeDuplicatedWhenInserted(request.RequestDto.Email);

            var member = _mapper.Map<Member>(request.RequestDto);
            await _memberRepository.AddAsync(member);

            return _mapper.Map<MemberResponseDto>(member);
        }
    }

    public class CreateMemberCommandValidator : AbstractValidator<CreateMemberRequestDto>
    {
        public CreateMemberCommandValidator()
        {
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