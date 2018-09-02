﻿namespace NasleGhalam.Common
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

        ProvinceReadAccess = 9,
        ProvinceCreateAccess = 10,
        ProvinceUpdateAccess = 11,
        ProvinceDeleteAccess = 12,

        CityReadAccess = 13,
        CityCreateAccess = 14,
        CityUpdateAccess = 15,
        CityDeleteAccess = 16,

        EducationGroup_LessonCreateAccess = 13,
        EducationGroup_LessonReadAccess = 14,
        EducationGroup_LessonUpdateAccess = 15,
        EducationGroup_LessonDeleteAccess = 16,

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
        UserDeleteAccess = 33,

        TopicCreateAccess = 34,
        TopicReadAccess = 35,
        TopicUpdateAccess = 36,
        TopicDeleteAccess = 37,

        EducationSubGroupReadAccess = 38,
        EducationSubGroupCreateAccess = 39,
        EducationSubGroupUpdateAccess = 40,
        EducationSubGroupDeleteAccess = 41,

        EducationYearReadAccess = 42,
        EducationYearCreateAccess = 43,
        EducationYearUpdateAccess = 44,
        EducationYearDeleteAccess = 45,

        ExamReadAccess = 46,
        ExamCreateAccess = 47,
        ExamUpdateAccess = 48,
        ExamDeleteAccess = 49,

        RatioReadAccess = 50,
        RatioCreateAccess = 51,
        RatioUpdateAccess = 52,
        RatioDeleteAccess = 53,

        QuestionCreateAccess = 54,
        QuestionUpdateAccess = 55,
        QuestionDeleteAccess = 56,
        QuestionReadAccess = 57,

        LookupReadAccess = 58,
        LookupCreateAccess = 59,
        LookupUpdateAccess = 60,
        LookupDeleteAccess = 61,

        PublisherReadAccess = 62,
        PublisherCreateAccess = 63,
        PublisherUpdateAccess = 64,
        PublisherDeleteAccess = 65,
    }
}
