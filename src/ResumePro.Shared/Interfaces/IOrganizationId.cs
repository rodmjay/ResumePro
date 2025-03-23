namespace ResumePro.Shared.Interfaces;

public interface IOrganizationId
{
    [JsonIgnore] int OrganizationId { get; set; }
}