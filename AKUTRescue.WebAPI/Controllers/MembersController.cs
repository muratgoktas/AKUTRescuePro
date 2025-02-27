using Microsoft.AspNetCore.Mvc;
using AKUTRescue.Application.Features.Members.Commands.CreateMember;
using AKUTRescue.Application.Features.Members.Commands.UpdateMember;
using AKUTRescue.Application.Features.Members.Commands.DeleteMember;
using AKUTRescue.Application.Features.Members.Queries.GetMemberList;
using AKUTRescue.Application.Features.Members.Queries.GetMemberById;


namespace AKUTRescue.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] bool? isActive, [FromQuery] Guid? teamId)
    {
        var query = new GetMemberListQuery { IsActive = isActive, TeamId = teamId };
        var result = await Mediator.Send(query);
        return Success(data: result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetMemberByIdQuery { Id = id };
        var result = await Mediator.Send(query);
        return Success(data: result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMemberRequestDto requestDto)
    {
        var command = new CreateMemberCommand(requestDto);
        var result = await Mediator.Send(command);
        return Created(data: result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMemberRequestDto requestDto)
    {
        requestDto.Id = id;
        var command = new UpdateMemberCommand { RequestDto = requestDto };
        var result = await Mediator.Send(command);
        return Success(data: result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteMemberCommand { Id = id };
        var result = await Mediator.Send(command);
        return Success(data: result);
    }
} 