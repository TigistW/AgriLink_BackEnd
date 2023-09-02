using MediatR;

namespace Api.Controllers;

public class FarmController : BaseApiController
{
    private readonly IMediator _mediator;

    public FarmController(IMediator mediator)
    {
        _mediator = mediator;
    }


    // [AllowAnonymous]
    // [HttpPost("add_mamibet")]
    // public async Task<ActionResult<Result<string>>> Register([FromForm] CreateMamibetDto createMamibetDto)
    // {
    //     var command = new CreateMamibetCommand { createMamibetDto = createMamibetDto };
    //     return HandleResult(await _mediator.Send(command));
    // }

}