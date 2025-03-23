namespace ResumePro.Shared.Models;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class ChatResult
{
    public bool Succeeded => !string.IsNullOrWhiteSpace(OutputText);

    public string OutputText { get; set; } = null!;
}