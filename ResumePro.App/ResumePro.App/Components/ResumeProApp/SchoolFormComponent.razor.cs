using ResumePro.App.Components.ResumeProApp.Bases;
using ResumePro.Shared.Options;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class SchoolFormComponent : FormComponent<SchoolOptions>
    {

        void AddDegree()
        {
            Options.Degrees.Add(new DegreeOptions());
        }

        void RemoveDegree(DegreeOptions degree)
        {
            Options.Degrees.Remove(degree);
        }

    }
}
