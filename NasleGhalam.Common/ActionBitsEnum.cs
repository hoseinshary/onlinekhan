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


        GradeReadAccess = 9,
        GradeCreateAccess = 10,
        GradeUpdateAccess = 11,
        GradeDeleteAccess = 12,

        GradeReadAccess = 17,
        GradeCreateAccess = 18,
        GradeUpdateAccess = 19,
        GradeDeleteAccess = 20,

        GradeLevelReadAccess = 21,
        GradeLevelCreateAccess = 22,
        GradeLevelUpdateAccess = 23,
        GradeLevelDeleteAccess = 24,

        RoleReadAccess = 25,
        RoleCreateAccess = 26,
        RoleUpdateAccess = 27,
        RoleDeleteAccess = 28,
        RoleChangeAccess = 29,

        UserReadAccess = 30,
        UserCreateAccess = 31,
        UserUpdateAccess = 32,
        UserDeleteAccess = 33
    }
}
