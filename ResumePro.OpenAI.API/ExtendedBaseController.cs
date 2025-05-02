using ResumePro.Shared.Extensions;
using TechValley.Rest.Bases;

namespace ResumePro.OpenAI.Api
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
