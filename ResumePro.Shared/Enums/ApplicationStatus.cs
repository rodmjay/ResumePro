using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumePro.Shared.Enums
{
    public enum ApplicationStatus : int
    {
        Applied = 0,
        InReview = 1,
        Interviewing = 2,
        Offered = 3,
        Rejected = 4
    }
}
