namespace ResumePro.Api.Interfaces;

public interface IOrganizationSettingsController
{
    Task<ActionResult<OrganizationSettingsDto>> CreateSettings(
        OrganizationSettingsOptions options);

    Task<ActionResult<OrganizationSettingsDto>> UpdateSettings(
        OrganizationSettingsOptions options);
}