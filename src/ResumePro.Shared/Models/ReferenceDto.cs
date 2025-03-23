namespace ResumePro.Shared.Models;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class ReferenceDto : IReference
{
    [JsonIgnore] public int PersonId { get; set; }

    public int Id { get; set; }

    public string Text { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public int Order { get; set; }

    [JsonIgnore] public int OrganizationId { get; set; }
}