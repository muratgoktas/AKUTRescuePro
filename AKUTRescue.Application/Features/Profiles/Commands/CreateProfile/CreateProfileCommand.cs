using System;
using System.Threading;
using System.Threading.Tasks;
using AKUTRescue.Application.Features.Profiles.Rules;
using AKUTRescue.Application.Services.Repositories;
using AKUTRescue.Domain.Entities;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AKUTRescue.Application.Features.Profiles.Commands.CreateProfile
{
    public class CreateProfileCommand : IRequest<ProfileResponseDto>
    {
        public CreateProfileRequestDto RequestDto { get; set; }

        public CreateProfileCommand(CreateProfileRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    }

    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, ProfileResponseDto>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMemberDetailRepository _memberDetailRepository;
        private readonly IMapper _mapper;
        private readonly ProfileBusinessRules _profileBusinessRules;
        private readonly CreateProfileCommandValidator _validator;

        public CreateProfileCommandHandler(
            IMemberRepository memberRepository,
            IMemberDetailRepository memberDetailRepository,
            IMapper mapper,
            ProfileBusinessRules profileBusinessRules)
        {
            _memberRepository = memberRepository;
            _memberDetailRepository = memberDetailRepository;
            _mapper = mapper;
            _profileBusinessRules = profileBusinessRules;
            _validator = new CreateProfileCommandValidator();
        }

        public async Task<ProfileResponseDto> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.RequestDto, cancellationToken);
            await _profileBusinessRules.EmailCannotBeDuplicatedWhenInserted(request.RequestDto.Email);

            var member = _mapper.Map<Member>(request.RequestDto);
            await _memberRepository.AddAsync(member);

            var memberDetail = _mapper.Map<MemberDetail>(request.RequestDto);
            memberDetail.MemberId = member.Id;
            await _memberDetailRepository.AddAsync(memberDetail);

            return _mapper.Map<ProfileResponseDto>((member, memberDetail));
        }
    }

    public class CreateProfileCommandValidator : AbstractValidator<CreateProfileRequestDto>
    {
        public CreateProfileCommandValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(p => p.LastName).NotEmpty().MaximumLength(50);
            RuleFor(p => p.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(p => p.PhoneNumber).NotEmpty().MaximumLength(20);
            RuleFor(p => p.IdentityNumber).NotEmpty().MaximumLength(20);
            RuleFor(p => p.DateOfBirth).NotEmpty().LessThan(DateTime.Now);
            RuleFor(p => p.BloodType).NotEmpty().MaximumLength(5);
            RuleFor(p => p.EmergencyContact).NotEmpty().MaximumLength(100);
            RuleFor(p => p.EmergencyPhone).NotEmpty().MaximumLength(20);
        }
    }
} 