namespace ResumePro.Shared.Models;

public class SchoolDto : ISchool
{
    public string DisplayEndDate
    {
        get
        {
            if (EndDate == null) return "Present";

            return EndDate.Value.ToShortDateString();
        }
    }

    public int Id { get; set; }

    [JsonIgnore] public int PersonId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public int OrganizationId { get; set; }
}