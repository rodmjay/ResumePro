using Microsoft.AspNetCore.Mvc.ModelBinding;
using ResumePro.Api.Interfaces;
using ResumePro.Services.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/settings")]
public sealed class OrganizationSettingsController : BaseController, IOrganizationSettingsController
{
    private readonly IOrganizationSettingsService _service;

    public OrganizationSettingsController(IServiceProvider serviceProvider,
        IOrganizationSettingsService service) : base(serviceProvider)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<OrganizationSettingsDto>> CreateSettings(
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)]
        OrganizationSettingsOptions? options = null)
    {
        options ??= new OrganizationSettingsOptions();

        var result = await _service.AddOrUpdateUpdateOrganizationSettings(OrganizationId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut]
    public async Task<ActionResult<OrganizationSettingsDto>> UpdateSettings(
        [FromBody] OrganizationSettingsOptions options)
    {
        var result = await _service.AddOrUpdateUpdateOrganizationSettings(OrganizationId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }
}