using System;

namespace ResumePro.Domain.Utilities
{
    public static class DateTimeUtilities
    {
        /// <summary>
        /// Ensures a DateTime value is in UTC format for PostgreSQL compatibility
        /// </summary>
        /// <param name="dateTime">The DateTime to convert</param>
        /// <returns>The DateTime in UTC format</returns>
        public static DateTime EnsureUtc(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            }
            
            return dateTime.Kind == DateTimeKind.Local 
                ? dateTime.ToUniversalTime() 
                : dateTime;
        }

        /// <summary>
        /// Ensures a nullable DateTime value is in UTC format for PostgreSQL compatibility
        /// </summary>
        /// <param name="dateTime">The nullable DateTime to convert</param>
        /// <returns>The nullable DateTime in UTC format</returns>
        public static DateTime? EnsureUtc(this DateTime? dateTime)
        {
            return dateTime.HasValue ? EnsureUtc(dateTime.Value) : null;
        }
    }
}
