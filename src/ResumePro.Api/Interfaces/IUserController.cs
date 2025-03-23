namespace ResumePro.Api.Interfaces;

public interface IUserController
{
    Task<UserOutput> GetUser();
}