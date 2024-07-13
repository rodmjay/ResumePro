using ResumePro.Shared.Common;

namespace ResumePro.ErrorDescribers
{
    public class ResumeErrorDescriber
    {
        public virtual Error ResumeNotFound(int resumeId)
        {
            return new Error()
            {
                Code = nameof(ResumeNotFound)
            };
        }

        public virtual Error UnableToSaveResume()
        {
            return new Error()
            {
                Code = nameof(UnableToSaveResume)
            };
        }
    }
}
