#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Text;

namespace ResumePro.Shared.Extensions;

public static class ObjectExtensions
{
    public static StringContent SerializeToUTF8Json(this object model)
    {
        var str = JsonConvert.SerializeObject(model);

        return new StringContent(str,
            Encoding.UTF8, "application/json");
    }
}