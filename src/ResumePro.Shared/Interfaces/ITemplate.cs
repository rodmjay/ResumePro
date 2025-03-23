namespace ResumePro.Shared.Interfaces;

public interface ITemplate
{
    string Name { get; set; }
    string Source { get; set; }
    string Format { get; set; }
    string Engine { get; set; }
    int Id { get; set; }
}