using ResumePro.Shared.Extensions;

namespace ResumePro.Api
{
    public class ExtendedBaseController : BaseController
    {

        public int UserId => User.UserId();

        public int OrganizationId => User.OrganizationId();

        protected ExtendedBaseController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
