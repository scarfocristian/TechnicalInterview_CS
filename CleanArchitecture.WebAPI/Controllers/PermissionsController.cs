using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.PermissionsFeatures.AddPermission;
using CleanArchitecture.Application.PermissionsFeatures.GetAllPermissions;
using CleanArchitecture.Application.PermissionsFeatures.UpdatePermission;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace CleanArchitecture.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PermissionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PermissionsController> _logger;

    public PermissionsController(IMediator mediator,
        ILogger<PermissionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<PermissionsDto>>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Get all Permissions.");

        var response = await _mediator.Send(new GetAllPermissionsQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<PermissionsDto>> Create(AddPermissionCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Create Permission.");

        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PermissionsDto>> Update(int id, UpdatePermissionCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Update Permission.");

        if (id != request.Id)
        {
            _logger.LogError($"The Id is different from the request.");
            return BadRequest();
        }

        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}