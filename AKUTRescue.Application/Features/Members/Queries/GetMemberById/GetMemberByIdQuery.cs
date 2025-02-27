using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AKUTRescue.Application.Features.Members.Rules;
using AKUTRescue.Application.Services.Repositories;
using FluentValidation;

namespace AKUTRescue.Application.Features.Members.Queries.GetMemberById;

public class GetMemberByIdQuery : IRequest<MemberResponseDto>
{
    public Guid Id { get; set; }
}

public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, MemberResponseDto>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;
    private readonly MemberBusinessRules _memberBusinessRules;

    public GetMemberByIdQueryHandler(
        IMemberRepository memberRepository,
        IMapper mapper,
        MemberBusinessRules memberBusinessRules)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
        _memberBusinessRules = memberBusinessRules;
    }

    public async Task<MemberResponseDto> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        // İş kuralı kontrolü
        await _memberBusinessRules.MemberShouldExistWhenRequested(request.Id);

        // İlişkili verileri de içerecek şekilde üyeyi getir
        var member = await _memberRepository.GetAsync(
            predicate: m => m.Id == request.Id,
            include: q => q
                .Include(m => m.Team)
                .Include(m => m.Authority),
            cancellationToken: cancellationToken
        );

        // DTO'ya dönüştür ve döndür
        return _mapper.Map<MemberResponseDto>(member);
    }
}

public class GetMemberByIdQueryValidator : AbstractValidator<GetMemberByIdQuery>
{
    public GetMemberByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Üye ID'si boş olamaz.");
    }
} 