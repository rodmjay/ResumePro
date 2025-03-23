namespace ResumePro.Api.Interfaces;

public interface ITextController
{
    Task<ChatResult> Professionalize([FromBody] ChatOptions options);
}