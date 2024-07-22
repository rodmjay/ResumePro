using Microsoft.AspNetCore.Components;
using ResumePro.App.Components.ResumeProApp.Bases;
using ResumePro.Shared.Options;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class SchoolFormComponent : FormComponent<SchoolOptions>
    {
        void AddDegree()
        {
            Options.DegreeOptions.Add(new DegreeOptions());
        }

        void RemoveDegree(DegreeOptions degree)
        {
            Options.DegreeOptions.Remove(degree);
        }


        private void MoveDegreeUp(int index)
        {
            if (index >= 0 && index < Options.DegreeOptions.Count - 1)
            {
                var item = Options.DegreeOptions[index];
                Options.DegreeOptions.RemoveAt(index);
                Options.DegreeOptions.Insert(index + 1, item);
            }
        }

        private void MoveDegreeDown(int index)
        {
            if (index >= 0 && index < Options.DegreeOptions.Count - 1)
            {
                var item = Options.DegreeOptions[index];
                Options.DegreeOptions.RemoveAt(index);
                Options.DegreeOptions.Insert(index + 1, item);
            }
        }
    }
}
