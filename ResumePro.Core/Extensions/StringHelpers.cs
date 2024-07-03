#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Core.Extensions;

public static class StringHelpers
{
    public static bool IsAscii(this string str)
    {
        foreach (var c in str)
            if (c > 127)
                return false;

        return true;
    }
}