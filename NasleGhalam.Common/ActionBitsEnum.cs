using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.Common
{
    public enum ActionBits
    {
        PublicAcceess = 0,

        LessonReadAccess = 1,
        LessonCreateAccess = 2,
        LessonUpdateAccess = 3,
        LessonDeleteAccess = 4,

        EducationGroupReadAccess = 5,
        EducationGroupCreateAccess = 6,
        EducationGroupUpdateAccess = 7,
        EducationGroupDeleteAccess = 8,
    }
}
