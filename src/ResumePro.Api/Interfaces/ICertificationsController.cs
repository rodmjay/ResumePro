namespace ResumePro.Api.Interfaces;

public interface ICertificationsController
{
    Task<CertificationDto> Get(int personId, int certificationId);
    Task<List<CertificationDto>> Get(int personId);

    Task<ActionResult<CertificationDto>> CreateCertification(int personId,
        CertificationOptions options);

    Task<ActionResult<CertificationDto>> Update(int personId, int certificationId,
        CertificationOptions options);

    Task<Result> Delete(int personId, int certificationId);
}