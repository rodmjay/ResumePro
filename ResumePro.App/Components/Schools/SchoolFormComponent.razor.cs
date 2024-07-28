#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.App.Components.Bases;
using ResumePro.Shared.Options;

namespace ResumePro.App.Components.Schools;

public partial class SchoolFormComponent : FormComponent<SchoolOptions>
{
    private void AddDegree()
    {
        Options.DegreeOptions.Add(new DegreeOptions());
    }

    private void RemoveDegree(DegreeOptions degree)
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