namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class initDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FaName = c.String(nullable: false, maxLength: 50),
                        ActionBit = c.Short(nullable: false),
                        Priority = c.Byte(nullable: false),
                        IsIndex = c.Boolean(nullable: false),
                        ControllerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Controllers", t => t.ControllerId)
                .Index(t => t.ControllerId);
            
            CreateTable(
                "dbo.Controllers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FaName = c.String(nullable: false, maxLength: 50),
                        EnName = c.String(nullable: false, maxLength: 50),
                        Icone = c.String(nullable: false, maxLength: 200),
                        Priority = c.Byte(nullable: false),
                        ModuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modules", t => t.ModuleId)
                .Index(t => t.ModuleId);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Priority = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_Module_Name");
            
            CreateTable(
                "dbo.EducationTrees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        ParentEducationTreeId = c.Int(),
                        LookupId_EducationTreeState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_EducationTreeState)
                .ForeignKey("dbo.EducationTrees", t => t.ParentEducationTreeId)
                .Index(t => t.ParentEducationTreeId)
                .Index(t => t.LookupId_EducationTreeState);
            
            CreateTable(
                "dbo.EducationSubGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        EducationTreeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationTrees", t => t.EducationTreeId)
                .Index(t => t.EducationTreeId);
            
            CreateTable(
                "dbo.HistoryEducations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        RankGoal = c.Int(nullable: false),
                        ExamId = c.Int(nullable: false),
                        EducationTreeId = c.Int(nullable: false),
                        EducationSubGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationTrees", t => t.EducationTreeId)
                .ForeignKey("dbo.Exams", t => t.ExamId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .ForeignKey("dbo.EducationSubGroups", t => t.EducationSubGroup_Id)
                .Index(t => t.StudentId)
                .Index(t => t.ExamId)
                .Index(t => t.EducationTreeId)
                .Index(t => t.EducationSubGroup_Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ProvinceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId)
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Code = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_Province_Name");
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Family = c.String(nullable: false, maxLength: 50),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        NationalNo = c.String(maxLength: 10),
                        Gender = c.Boolean(nullable: false),
                        Phone = c.String(maxLength: 8),
                        Mobile = c.String(maxLength: 10),
                        IsAdmin = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        LastLogin = c.DateTime(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.Username, unique: true, name: "UK_User_Username")
                .Index(t => t.NationalNo, unique: true, name: "UK_User_NationalNo")
                .Index(t => t.RoleId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.QuestionAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Context = c.String(nullable: false),
                        FilePath = c.String(maxLength: 200),
                        LookupId_AnswerType = c.Int(nullable: false),
                        Description = c.String(maxLength: 300),
                        Author = c.String(maxLength: 100),
                        IsMaster = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_AnswerType)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.LookupId_AnswerType)
                .Index(t => t.UserId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Lookups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Value = c.String(nullable: false, maxLength: 50),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AxillaryBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        PublishYear = c.Short(nullable: false),
                        Author = c.String(nullable: false, maxLength: 100),
                        Isbn = c.String(nullable: false, maxLength: 100),
                        Font = c.String(maxLength: 50),
                        ImgName = c.String(maxLength: 200),
                        Price = c.Int(nullable: false),
                        OriginalPrice = c.Int(nullable: false),
                        Description = c.String(maxLength: 300),
                        LookupId_PrintType = c.Int(nullable: false),
                        PublisherId = c.Int(nullable: false),
                        LookupId_PaperType = c.Int(nullable: false),
                        LookupId_BookType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_BookType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_PaperType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_PrintType)
                .ForeignKey("dbo.Publishers", t => t.PublisherId)
                .Index(t => t.Name, unique: true, name: "UK_AxillaryBook_Name")
                .Index(t => t.LookupId_PrintType)
                .Index(t => t.PublisherId)
                .Index(t => t.LookupId_PaperType)
                .Index(t => t.LookupId_BookType);
            
            CreateTable(
                "dbo.EducationBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        PublishYear = c.Short(nullable: false),
                        IsExamSource = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsChanged = c.Boolean(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.Name, unique: true, name: "UK_EducationBook_Name")
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        IsMain = c.Boolean(nullable: false),
                        LookupId_Nezam = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_Nezam)
                .Index(t => t.Name, unique: true, name: "UK_Lesson_Name")
                .Index(t => t.LookupId_Nezam);
            
            CreateTable(
                "dbo.QuestionGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        InsertTime = c.DateTime(nullable: false),
                        File = c.String(maxLength: 50),
                        LessonId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.LessonId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Context = c.String(nullable: false),
                        QuestionNumber = c.Int(nullable: false),
                        LookupId_QuestionType = c.Int(nullable: false),
                        QuestionPoint = c.Int(nullable: false),
                        LookupId_QuestionHardnessType = c.Int(nullable: false),
                        LookupId_RepeatnessType = c.Int(nullable: false),
                        UseEvaluation = c.Boolean(nullable: false),
                        IsStandard = c.Boolean(nullable: false),
                        LookupId_AuthorType = c.Int(nullable: false),
                        AuthorName = c.String(maxLength: 100),
                        ResponseSecond = c.Short(nullable: false),
                        LookupId_AreaType = c.Int(nullable: false),
                        Description = c.String(maxLength: 300),
                        FileName = c.String(nullable: false, maxLength: 50),
                        InsertDateTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsExercise = c.Boolean(nullable: false),
                        IsLearning = c.Boolean(nullable: false),
                        AnswerNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId_AreaType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_AuthorType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_QuestionHardnessType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_QuestionType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_RepeatnessType)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.LookupId_QuestionType)
                .Index(t => t.LookupId_QuestionHardnessType)
                .Index(t => t.LookupId_RepeatnessType)
                .Index(t => t.LookupId_AuthorType)
                .Index(t => t.LookupId_AreaType)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Boxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FatherName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 300),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId, unique: true);
            
            CreateTable(
                "dbo.QuestionEquals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EqualType = c.Byte(nullable: false),
                        QuestionId1 = c.Int(nullable: false),
                        QuestionId2 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId1)
                .ForeignKey("dbo.Questions", t => t.QuestionId2)
                .Index(t => t.QuestionId1)
                .Index(t => t.QuestionId2);
            
            CreateTable(
                "dbo.QuestionJudges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LookupId_QuestionHardnessType = c.Int(nullable: false),
                        LookupId_RepeatnessType = c.Int(nullable: false),
                        Repeatness = c.Byte(nullable: false),
                        IsStandard = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        IsUpdate = c.Boolean(nullable: false),
                        IsLearning = c.Boolean(nullable: false),
                        ResponseSecond = c.Short(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        Lookup_QuestionHardnessType_Id = c.Int(),
                        Lookup_RepeatnessType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.Lookup_QuestionHardnessType_Id)
                .ForeignKey("dbo.Lookups", t => t.Lookup_RepeatnessType_Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId)
                .Index(t => t.Lookup_QuestionHardnessType_Id)
                .Index(t => t.Lookup_RepeatnessType_Id);
            
            CreateTable(
                "dbo.QuestionOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Context = c.String(nullable: false),
                        IsAnswer = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsSource = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_Tag_Name");
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        ExamStock = c.Int(nullable: false),
                        ExamStockSystem = c.Int(nullable: false),
                        Importance = c.Short(nullable: false),
                        IsExamSource = c.Boolean(nullable: false),
                        LookupId_HardnessType = c.Int(nullable: false),
                        LookupId_AreaType = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ParentTopicId = c.Int(),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .ForeignKey("dbo.Lookups", t => t.LookupId_AreaType)
                .ForeignKey("dbo.Lookups", t => t.LookupId_HardnessType)
                .ForeignKey("dbo.Topics", t => t.ParentTopicId)
                .Index(t => t.LookupId_HardnessType)
                .Index(t => t.LookupId_AreaType)
                .Index(t => t.ParentTopicId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Ratios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rate = c.Byte(nullable: false),
                        LessonId = c.Int(nullable: false),
                        EducationSubGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationSubGroups", t => t.EducationSubGroupId)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.LessonId)
                .Index(t => t.EducationSubGroupId);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_Publisher_Name");
            
            CreateTable(
                "dbo.QuestionAnswerViews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rate = c.Byte(nullable: false),
                        AnswerId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionAnswers", t => t.AnswerId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.AnswerId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FatherName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Level = c.Byte(nullable: false),
                        SumOfActionBit = c.String(nullable: false, maxLength: 300),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_Role_Name");
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Date = c.DateTime(nullable: false),
                        EducationTreeId = c.Int(nullable: false),
                        EducationYearId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationTrees", t => t.EducationTreeId)
                .ForeignKey("dbo.EducationYears", t => t.EducationYearId)
                .Index(t => t.EducationTreeId)
                .Index(t => t.EducationYearId);
            
            CreateTable(
                "dbo.EducationYears",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsActiveYear = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_EducationYear_Name");
            
            CreateTable(
                "dbo.UniversityBranches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        SiteAverage = c.Int(nullable: false),
                        Balance1Low = c.Int(nullable: false),
                        Balance1High = c.Int(nullable: false),
                        Balance2Low = c.Int(nullable: false),
                        Balance2High = c.Int(nullable: false),
                        EducationSubGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationSubGroups", t => t.EducationSubGroupId)
                .Index(t => t.Name, unique: true, name: "UK_UniversityBranch_Name")
                .Index(t => t.EducationSubGroupId);
            
            CreateTable(
                "dbo.Questions_Boxes",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        BoxId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.BoxId })
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Boxes", t => t.BoxId)
                .Index(t => t.QuestionId)
                .Index(t => t.BoxId);
            
            CreateTable(
                "dbo.Questions_Tags",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.TagId })
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Tags", t => t.TagId)
                .Index(t => t.QuestionId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Topics_EducationBooks",
                c => new
                    {
                        TopicId = c.Int(nullable: false),
                        EducationBookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TopicId, t.EducationBookId })
                .ForeignKey("dbo.Topics", t => t.TopicId)
                .ForeignKey("dbo.EducationBooks", t => t.EducationBookId)
                .Index(t => t.TopicId)
                .Index(t => t.EducationBookId);
            
            CreateTable(
                "dbo.Topics_Questions",
                c => new
                    {
                        TopicId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TopicId, t.QuestionId })
                .ForeignKey("dbo.Topics", t => t.TopicId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.TopicId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.QuestionGroups_Questions",
                c => new
                    {
                        QuestionGroupId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionGroupId, t.QuestionId })
                .ForeignKey("dbo.QuestionGroups", t => t.QuestionGroupId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionGroupId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.AxillaryBooks_EducationBooks",
                c => new
                    {
                        AxillaryBookId = c.Int(nullable: false),
                        EducationBookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AxillaryBookId, t.EducationBookId })
                .ForeignKey("dbo.AxillaryBooks", t => t.AxillaryBookId)
                .ForeignKey("dbo.EducationBooks", t => t.EducationBookId)
                .Index(t => t.AxillaryBookId)
                .Index(t => t.EducationBookId);
            
            CreateTable(
                "dbo.HistoryEducations_Cities",
                c => new
                    {
                        HistoryEducationId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HistoryEducationId, t.CityId })
                .ForeignKey("dbo.HistoryEducations", t => t.HistoryEducationId)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.HistoryEducationId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.HistoryEducations_UniversityBranchs",
                c => new
                    {
                        HistoryEducationId = c.Int(nullable: false),
                        UniversityBranchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HistoryEducationId, t.UniversityBranchId })
                .ForeignKey("dbo.HistoryEducations", t => t.HistoryEducationId)
                .ForeignKey("dbo.UniversityBranches", t => t.UniversityBranchId)
                .Index(t => t.HistoryEducationId)
                .Index(t => t.UniversityBranchId);
            
            CreateTable(
                "dbo.EducationTrees_Lessons",
                c => new
                    {
                        EducationTreeId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EducationTreeId, t.LessonId })
                .ForeignKey("dbo.EducationTrees", t => t.EducationTreeId)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.EducationTreeId)
                .Index(t => t.LessonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EducationTrees", "ParentEducationTreeId", "dbo.EducationTrees");
            DropForeignKey("dbo.EducationTrees", "LookupId_EducationTreeState", "dbo.Lookups");
            DropForeignKey("dbo.EducationTrees_Lessons", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.EducationTrees_Lessons", "EducationTreeId", "dbo.EducationTrees");
            DropForeignKey("dbo.HistoryEducations", "EducationSubGroup_Id", "dbo.EducationSubGroups");
            DropForeignKey("dbo.HistoryEducations_UniversityBranchs", "UniversityBranchId", "dbo.UniversityBranches");
            DropForeignKey("dbo.HistoryEducations_UniversityBranchs", "HistoryEducationId", "dbo.HistoryEducations");
            DropForeignKey("dbo.UniversityBranches", "EducationSubGroupId", "dbo.EducationSubGroups");
            DropForeignKey("dbo.HistoryEducations", "StudentId", "dbo.Students");
            DropForeignKey("dbo.HistoryEducations", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Exams", "EducationYearId", "dbo.EducationYears");
            DropForeignKey("dbo.Exams", "EducationTreeId", "dbo.EducationTrees");
            DropForeignKey("dbo.HistoryEducations", "EducationTreeId", "dbo.EducationTrees");
            DropForeignKey("dbo.HistoryEducations_Cities", "CityId", "dbo.Cities");
            DropForeignKey("dbo.HistoryEducations_Cities", "HistoryEducationId", "dbo.HistoryEducations");
            DropForeignKey("dbo.Students", "Id", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.QuestionAnswers", "UserId", "dbo.Users");
            DropForeignKey("dbo.QuestionAnswerViews", "StudentId", "dbo.Students");
            DropForeignKey("dbo.QuestionAnswerViews", "AnswerId", "dbo.QuestionAnswers");
            DropForeignKey("dbo.QuestionAnswers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionAnswers", "LookupId_AnswerType", "dbo.Lookups");
            DropForeignKey("dbo.AxillaryBooks", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.AxillaryBooks", "LookupId_PrintType", "dbo.Lookups");
            DropForeignKey("dbo.AxillaryBooks", "LookupId_PaperType", "dbo.Lookups");
            DropForeignKey("dbo.AxillaryBooks", "LookupId_BookType", "dbo.Lookups");
            DropForeignKey("dbo.AxillaryBooks_EducationBooks", "EducationBookId", "dbo.EducationBooks");
            DropForeignKey("dbo.AxillaryBooks_EducationBooks", "AxillaryBookId", "dbo.AxillaryBooks");
            DropForeignKey("dbo.EducationBooks", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Ratios", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Ratios", "EducationSubGroupId", "dbo.EducationSubGroups");
            DropForeignKey("dbo.QuestionGroups", "UserId", "dbo.Users");
            DropForeignKey("dbo.QuestionGroups_Questions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionGroups_Questions", "QuestionGroupId", "dbo.QuestionGroups");
            DropForeignKey("dbo.Questions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Topics_Questions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Topics_Questions", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Topics", "ParentTopicId", "dbo.Topics");
            DropForeignKey("dbo.Topics", "LookupId_HardnessType", "dbo.Lookups");
            DropForeignKey("dbo.Topics", "LookupId_AreaType", "dbo.Lookups");
            DropForeignKey("dbo.Topics", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Topics_EducationBooks", "EducationBookId", "dbo.EducationBooks");
            DropForeignKey("dbo.Topics_EducationBooks", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Questions_Tags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Questions_Tags", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionOptions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionJudges", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionJudges", "Lookup_RepeatnessType_Id", "dbo.Lookups");
            DropForeignKey("dbo.QuestionJudges", "Lookup_QuestionHardnessType_Id", "dbo.Lookups");
            DropForeignKey("dbo.QuestionEquals", "QuestionId2", "dbo.Questions");
            DropForeignKey("dbo.QuestionEquals", "QuestionId1", "dbo.Questions");
            DropForeignKey("dbo.Questions", "LookupId_RepeatnessType", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "LookupId_QuestionType", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "LookupId_QuestionHardnessType", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "LookupId_AuthorType", "dbo.Lookups");
            DropForeignKey("dbo.Questions", "LookupId_AreaType", "dbo.Lookups");
            DropForeignKey("dbo.Questions_Boxes", "BoxId", "dbo.Boxes");
            DropForeignKey("dbo.Questions_Boxes", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Boxes", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "UserId", "dbo.Users");
            DropForeignKey("dbo.QuestionGroups", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Lessons", "LookupId_Nezam", "dbo.Lookups");
            DropForeignKey("dbo.Users", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.EducationSubGroups", "EducationTreeId", "dbo.EducationTrees");
            DropForeignKey("dbo.Actions", "ControllerId", "dbo.Controllers");
            DropForeignKey("dbo.Controllers", "ModuleId", "dbo.Modules");
            DropIndex("dbo.EducationTrees_Lessons", new[] { "LessonId" });
            DropIndex("dbo.EducationTrees_Lessons", new[] { "EducationTreeId" });
            DropIndex("dbo.HistoryEducations_UniversityBranchs", new[] { "UniversityBranchId" });
            DropIndex("dbo.HistoryEducations_UniversityBranchs", new[] { "HistoryEducationId" });
            DropIndex("dbo.HistoryEducations_Cities", new[] { "CityId" });
            DropIndex("dbo.HistoryEducations_Cities", new[] { "HistoryEducationId" });
            DropIndex("dbo.AxillaryBooks_EducationBooks", new[] { "EducationBookId" });
            DropIndex("dbo.AxillaryBooks_EducationBooks", new[] { "AxillaryBookId" });
            DropIndex("dbo.QuestionGroups_Questions", new[] { "QuestionId" });
            DropIndex("dbo.QuestionGroups_Questions", new[] { "QuestionGroupId" });
            DropIndex("dbo.Topics_Questions", new[] { "QuestionId" });
            DropIndex("dbo.Topics_Questions", new[] { "TopicId" });
            DropIndex("dbo.Topics_EducationBooks", new[] { "EducationBookId" });
            DropIndex("dbo.Topics_EducationBooks", new[] { "TopicId" });
            DropIndex("dbo.Questions_Tags", new[] { "TagId" });
            DropIndex("dbo.Questions_Tags", new[] { "QuestionId" });
            DropIndex("dbo.Questions_Boxes", new[] { "BoxId" });
            DropIndex("dbo.Questions_Boxes", new[] { "QuestionId" });
            DropIndex("dbo.UniversityBranches", new[] { "EducationSubGroupId" });
            DropIndex("dbo.UniversityBranches", "UK_UniversityBranch_Name");
            DropIndex("dbo.EducationYears", "UK_EducationYear_Name");
            DropIndex("dbo.Exams", new[] { "EducationYearId" });
            DropIndex("dbo.Exams", new[] { "EducationTreeId" });
            DropIndex("dbo.Roles", "UK_Role_Name");
            DropIndex("dbo.Students", new[] { "Id" });
            DropIndex("dbo.QuestionAnswerViews", new[] { "StudentId" });
            DropIndex("dbo.QuestionAnswerViews", new[] { "AnswerId" });
            DropIndex("dbo.Publishers", "UK_Publisher_Name");
            DropIndex("dbo.Ratios", new[] { "EducationSubGroupId" });
            DropIndex("dbo.Ratios", new[] { "LessonId" });
            DropIndex("dbo.Topics", new[] { "LessonId" });
            DropIndex("dbo.Topics", new[] { "ParentTopicId" });
            DropIndex("dbo.Topics", new[] { "LookupId_AreaType" });
            DropIndex("dbo.Topics", new[] { "LookupId_HardnessType" });
            DropIndex("dbo.Tags", "UK_Tag_Name");
            DropIndex("dbo.QuestionOptions", new[] { "QuestionId" });
            DropIndex("dbo.QuestionJudges", new[] { "Lookup_RepeatnessType_Id" });
            DropIndex("dbo.QuestionJudges", new[] { "Lookup_QuestionHardnessType_Id" });
            DropIndex("dbo.QuestionJudges", new[] { "QuestionId" });
            DropIndex("dbo.QuestionEquals", new[] { "QuestionId2" });
            DropIndex("dbo.QuestionEquals", new[] { "QuestionId1" });
            DropIndex("dbo.Teachers", new[] { "UserId" });
            DropIndex("dbo.Boxes", new[] { "TeacherId" });
            DropIndex("dbo.Questions", new[] { "UserId" });
            DropIndex("dbo.Questions", new[] { "LookupId_AreaType" });
            DropIndex("dbo.Questions", new[] { "LookupId_AuthorType" });
            DropIndex("dbo.Questions", new[] { "LookupId_RepeatnessType" });
            DropIndex("dbo.Questions", new[] { "LookupId_QuestionHardnessType" });
            DropIndex("dbo.Questions", new[] { "LookupId_QuestionType" });
            DropIndex("dbo.QuestionGroups", new[] { "UserId" });
            DropIndex("dbo.QuestionGroups", new[] { "LessonId" });
            DropIndex("dbo.Lessons", new[] { "LookupId_Nezam" });
            DropIndex("dbo.Lessons", "UK_Lesson_Name");
            DropIndex("dbo.EducationBooks", new[] { "LessonId" });
            DropIndex("dbo.EducationBooks", "UK_EducationBook_Name");
            DropIndex("dbo.AxillaryBooks", new[] { "LookupId_BookType" });
            DropIndex("dbo.AxillaryBooks", new[] { "LookupId_PaperType" });
            DropIndex("dbo.AxillaryBooks", new[] { "PublisherId" });
            DropIndex("dbo.AxillaryBooks", new[] { "LookupId_PrintType" });
            DropIndex("dbo.AxillaryBooks", "UK_AxillaryBook_Name");
            DropIndex("dbo.QuestionAnswers", new[] { "QuestionId" });
            DropIndex("dbo.QuestionAnswers", new[] { "UserId" });
            DropIndex("dbo.QuestionAnswers", new[] { "LookupId_AnswerType" });
            DropIndex("dbo.Users", new[] { "CityId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Users", "UK_User_NationalNo");
            DropIndex("dbo.Users", "UK_User_Username");
            DropIndex("dbo.Provinces", "UK_Province_Name");
            DropIndex("dbo.Cities", new[] { "ProvinceId" });
            DropIndex("dbo.HistoryEducations", new[] { "EducationSubGroup_Id" });
            DropIndex("dbo.HistoryEducations", new[] { "EducationTreeId" });
            DropIndex("dbo.HistoryEducations", new[] { "ExamId" });
            DropIndex("dbo.HistoryEducations", new[] { "StudentId" });
            DropIndex("dbo.EducationSubGroups", new[] { "EducationTreeId" });
            DropIndex("dbo.EducationTrees", new[] { "LookupId_EducationTreeState" });
            DropIndex("dbo.EducationTrees", new[] { "ParentEducationTreeId" });
            DropIndex("dbo.Modules", "UK_Module_Name");
            DropIndex("dbo.Controllers", new[] { "ModuleId" });
            DropIndex("dbo.Actions", new[] { "ControllerId" });
            DropTable("dbo.EducationTrees_Lessons");
            DropTable("dbo.HistoryEducations_UniversityBranchs");
            DropTable("dbo.HistoryEducations_Cities");
            DropTable("dbo.AxillaryBooks_EducationBooks");
            DropTable("dbo.QuestionGroups_Questions");
            DropTable("dbo.Topics_Questions");
            DropTable("dbo.Topics_EducationBooks");
            DropTable("dbo.Questions_Tags");
            DropTable("dbo.Questions_Boxes");
            DropTable("dbo.UniversityBranches");
            DropTable("dbo.EducationYears");
            DropTable("dbo.Exams");
            DropTable("dbo.Roles");
            DropTable("dbo.Students");
            DropTable("dbo.QuestionAnswerViews");
            DropTable("dbo.Publishers");
            DropTable("dbo.Ratios");
            DropTable("dbo.Topics");
            DropTable("dbo.Tags");
            DropTable("dbo.QuestionOptions");
            DropTable("dbo.QuestionJudges");
            DropTable("dbo.QuestionEquals");
            DropTable("dbo.Teachers");
            DropTable("dbo.Boxes");
            DropTable("dbo.Questions");
            DropTable("dbo.QuestionGroups");
            DropTable("dbo.Lessons");
            DropTable("dbo.EducationBooks");
            DropTable("dbo.AxillaryBooks");
            DropTable("dbo.Lookups");
            DropTable("dbo.QuestionAnswers");
            DropTable("dbo.Users");
            DropTable("dbo.Provinces");
            DropTable("dbo.Cities");
            DropTable("dbo.HistoryEducations");
            DropTable("dbo.EducationSubGroups");
            DropTable("dbo.EducationTrees");
            DropTable("dbo.Modules");
            DropTable("dbo.Controllers");
            DropTable("dbo.Actions");
        }
    }
}
