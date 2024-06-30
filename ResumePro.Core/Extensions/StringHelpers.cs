namespace ResumePro.Core.Extensions
{
    public static class StringHelpers
    {
        public static bool IsAscii(this string str)
        {
            foreach (char c in str)
            {
                if(c > 127)
                    return false;
            }

            return true;
        }
    }
}
