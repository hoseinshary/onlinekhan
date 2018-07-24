namespace NasleGhalam.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Context = c.String(nullable: false),
                        FilePath = c.String(maxLength: 200),
                        AnswerType = c.Byte(nullable: false),
                        Description = c.String(maxLength: 300),
                        Author = c.String(maxLength: 100),
                        IsMaster = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Context = c.String(nullable: false),
                        QuestionNumber = c.Int(nullable: false),
                        QuestionType = c.Byte(nullable: false),
                        QuestionPoint = c.Int(nullable: false),
                        QuestionHardnessType = c.Byte(nullable: false),
                        ReapetnessType = c.Byte(nullable: false),
                        UseEvaluation = c.Boolean(nullable: false),
                        IsStandard = c.Boolean(nullable: false),
                        AuthorType = c.Byte(nullable: false),
                        AuthorName = c.String(nullable: false, maxLength: 100),
                        ResponseSecond = c.Short(nullable: false),
                        Description = c.String(maxLength: 300),
                        InsertDateTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        AxillaryBookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AxillaryBooks", t => t.AxillaryBookId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.AxillaryBookId);
            
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
                        PrintType = c.Byte(nullable: false),
                        ImgPath = c.String(maxLength: 50),
                        Price = c.Int(nullable: false),
                        OriginalPrice = c.Int(nullable: false),
                        PaperType = c.Byte(nullable: false),
                        HasImage = c.Boolean(nullable: false),
                        Description = c.String(nullable: false, maxLength: 300),
                        PublisherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publishers", t => t.PublisherId)
                .Index(t => t.PublisherId);
            
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
                        GradeLevelId = c.Int(nullable: false),
                        EducationGroup_LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationGroups_Lessons", t => t.EducationGroup_LessonId)
                .ForeignKey("dbo.GradeLevels", t => t.GradeLevelId)
                .Index(t => t.GradeLevelId)
                .Index(t => t.EducationGroup_LessonId);
            
            CreateTable(
                "dbo.EducationGroups_Lessons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EducationGroupId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationGroups", t => t.EducationGroupId)
                .ForeignKey("dbo.Lessons", t => t.LessonId)
                .Index(t => t.EducationGroupId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.EducationGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EducationSubGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        EducationGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationGroups", t => t.EducationGroupId, cascadeDelete: true)
                .Index(t => t.EducationGroupId);
            
            CreateTable(
                "dbo.HistoryEducations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        ExamId = c.Int(nullable: false),
                        EducationSubGroupId = c.Int(nullable: false),
                        EducationGroupId = c.Int(nullable: false),
                        GradeLevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationGroups", t => t.EducationGroupId)
                .ForeignKey("dbo.EducationSubGroups", t => t.EducationSubGroupId)
                .ForeignKey("dbo.Exams", t => t.ExamId)
                .ForeignKey("dbo.GradeLevels", t => t.GradeLevelId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.ExamId)
                .Index(t => t.EducationSubGroupId)
                .Index(t => t.EducationGroupId)
                .Index(t => t.GradeLevelId);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Date = c.DateTime(nullable: false),
                        EducationGroupId = c.Int(nullable: false),
                        EducationYearId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationGroups", t => t.EducationGroupId)
                .ForeignKey("dbo.EducationYears", t => t.EducationYearId)
                .Index(t => t.EducationGroupId)
                .Index(t => t.EducationYearId);
            
            CreateTable(
                "dbo.EducationYears",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsActiveYear = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GradeLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Priority = c.Byte(nullable: false),
                        GradeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grades", t => t.GradeId)
                .Index(t => t.GradeId);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Priority = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.UniversityBranches_HistoryEducations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UniversityBranchId = c.Int(nullable: false),
                        HistoryEducationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryEducations", t => t.HistoryEducationId)
                .ForeignKey("dbo.UniversityBranches", t => t.UniversityBranchId)
                .Index(t => t.UniversityBranchId)
                .Index(t => t.HistoryEducationId);
            
            CreateTable(
                "dbo.UniversityBranches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Balance = c.Int(nullable: false),
                        EducationSubGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationSubGroups", t => t.EducationSubGroupId)
                .Index(t => t.EducationSubGroupId);
            
            CreateTable(
                "dbo.Ratios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
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
                "dbo.Lessons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        IsMain = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        HardnessType = c.Byte(nullable: false),
                        AreaType = c.Byte(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ParentTopicId = c.Int(nullable: false),
                        EducationGroup_LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationGroups_Lessons", t => t.EducationGroup_LessonId)
                .ForeignKey("dbo.Topics", t => t.ParentTopicId)
                .Index(t => t.ParentTopicId)
                .Index(t => t.EducationGroup_LessonId);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Hardness = c.Byte(nullable: false),
                        Repeatness = c.Byte(nullable: false),
                        IsStandard = c.Boolean(nullable: false),
                        ResponseSecond = c.Short(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId);
            
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
                    })
                .PrimaryKey(t => t.Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionAnswers", "UserId", "dbo.Users");
            DropForeignKey("dbo.QuestionAnswers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Questions_Tags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Questions_Tags", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionOptions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionJudges", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionEquals", "QuestionId2", "dbo.Questions");
            DropForeignKey("dbo.QuestionEquals", "QuestionId1", "dbo.Questions");
            DropForeignKey("dbo.Questions_Boxes", "BoxId", "dbo.Boxes");
            DropForeignKey("dbo.Questions_Boxes", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Boxes", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "UserId", "dbo.Users");
            DropForeignKey("dbo.Questions", "AxillaryBookId", "dbo.AxillaryBooks");
            DropForeignKey("dbo.AxillaryBooks", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.AxillaryBooks_EducationBooks", "EducationBookId", "dbo.EducationBooks");
            DropForeignKey("dbo.AxillaryBooks_EducationBooks", "AxillaryBookId", "dbo.AxillaryBooks");
            DropForeignKey("dbo.EducationBooks", "GradeLevelId", "dbo.GradeLevels");
            DropForeignKey("dbo.EducationBooks", "EducationGroup_LessonId", "dbo.EducationGroups_Lessons");
            DropForeignKey("dbo.Topics", "ParentTopicId", "dbo.Topics");
            DropForeignKey("dbo.Topics", "EducationGroup_LessonId", "dbo.EducationGroups_Lessons");
            DropForeignKey("dbo.Topics_EducationBooks", "EducationBookId", "dbo.EducationBooks");
            DropForeignKey("dbo.Topics_EducationBooks", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.EducationGroups_Lessons", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.EducationGroups_Lessons", "EducationGroupId", "dbo.EducationGroups");
            DropForeignKey("dbo.Ratios", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Ratios", "EducationSubGroupId", "dbo.EducationSubGroups");
            DropForeignKey("dbo.UniversityBranches_HistoryEducations", "UniversityBranchId", "dbo.UniversityBranches");
            DropForeignKey("dbo.UniversityBranches", "EducationSubGroupId", "dbo.EducationSubGroups");
            DropForeignKey("dbo.UniversityBranches_HistoryEducations", "HistoryEducationId", "dbo.HistoryEducations");
            DropForeignKey("dbo.HistoryEducations", "StudentId", "dbo.Students");
            DropForeignKey("dbo.QuestionAnswerViews", "StudentId", "dbo.Students");
            DropForeignKey("dbo.QuestionAnswerViews", "AnswerId", "dbo.QuestionAnswers");
            DropForeignKey("dbo.HistoryEducations", "GradeLevelId", "dbo.GradeLevels");
            DropForeignKey("dbo.GradeLevels", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.HistoryEducations", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Exams", "EducationYearId", "dbo.EducationYears");
            DropForeignKey("dbo.Exams", "EducationGroupId", "dbo.EducationGroups");
            DropForeignKey("dbo.HistoryEducations", "EducationSubGroupId", "dbo.EducationSubGroups");
            DropForeignKey("dbo.HistoryEducations", "EducationGroupId", "dbo.EducationGroups");
            DropForeignKey("dbo.EducationSubGroups", "EducationGroupId", "dbo.EducationGroups");
            DropIndex("dbo.Questions_Tags", new[] { "TagId" });
            DropIndex("dbo.Questions_Tags", new[] { "QuestionId" });
            DropIndex("dbo.Questions_Boxes", new[] { "BoxId" });
            DropIndex("dbo.Questions_Boxes", new[] { "QuestionId" });
            DropIndex("dbo.AxillaryBooks_EducationBooks", new[] { "EducationBookId" });
            DropIndex("dbo.AxillaryBooks_EducationBooks", new[] { "AxillaryBookId" });
            DropIndex("dbo.Topics_EducationBooks", new[] { "EducationBookId" });
            DropIndex("dbo.Topics_EducationBooks", new[] { "TopicId" });
            DropIndex("dbo.QuestionOptions", new[] { "QuestionId" });
            DropIndex("dbo.QuestionJudges", new[] { "QuestionId" });
            DropIndex("dbo.QuestionEquals", new[] { "QuestionId2" });
            DropIndex("dbo.QuestionEquals", new[] { "QuestionId1" });
            DropIndex("dbo.Teachers", new[] { "UserId" });
            DropIndex("dbo.Boxes", new[] { "TeacherId" });
            DropIndex("dbo.Topics", new[] { "EducationGroup_LessonId" });
            DropIndex("dbo.Topics", new[] { "ParentTopicId" });
            DropIndex("dbo.Ratios", new[] { "EducationSubGroupId" });
            DropIndex("dbo.Ratios", new[] { "LessonId" });
            DropIndex("dbo.UniversityBranches", new[] { "EducationSubGroupId" });
            DropIndex("dbo.UniversityBranches_HistoryEducations", new[] { "HistoryEducationId" });
            DropIndex("dbo.UniversityBranches_HistoryEducations", new[] { "UniversityBranchId" });
            DropIndex("dbo.QuestionAnswerViews", new[] { "StudentId" });
            DropIndex("dbo.QuestionAnswerViews", new[] { "AnswerId" });
            DropIndex("dbo.GradeLevels", new[] { "GradeId" });
            DropIndex("dbo.Exams", new[] { "EducationYearId" });
            DropIndex("dbo.Exams", new[] { "EducationGroupId" });
            DropIndex("dbo.HistoryEducations", new[] { "GradeLevelId" });
            DropIndex("dbo.HistoryEducations", new[] { "EducationGroupId" });
            DropIndex("dbo.HistoryEducations", new[] { "EducationSubGroupId" });
            DropIndex("dbo.HistoryEducations", new[] { "ExamId" });
            DropIndex("dbo.HistoryEducations", new[] { "StudentId" });
            DropIndex("dbo.EducationSubGroups", new[] { "EducationGroupId" });
            DropIndex("dbo.EducationGroups_Lessons", new[] { "LessonId" });
            DropIndex("dbo.EducationGroups_Lessons", new[] { "EducationGroupId" });
            DropIndex("dbo.EducationBooks", new[] { "EducationGroup_LessonId" });
            DropIndex("dbo.EducationBooks", new[] { "GradeLevelId" });
            DropIndex("dbo.AxillaryBooks", new[] { "PublisherId" });
            DropIndex("dbo.Questions", new[] { "AxillaryBookId" });
            DropIndex("dbo.Questions", new[] { "UserId" });
            DropIndex("dbo.QuestionAnswers", new[] { "QuestionId" });
            DropIndex("dbo.QuestionAnswers", new[] { "UserId" });
            DropTable("dbo.Questions_Tags");
            DropTable("dbo.Questions_Boxes");
            DropTable("dbo.AxillaryBooks_EducationBooks");
            DropTable("dbo.Topics_EducationBooks");
            DropTable("dbo.Tags");
            DropTable("dbo.QuestionOptions");
            DropTable("dbo.QuestionJudges");
            DropTable("dbo.QuestionEquals");
            DropTable("dbo.Teachers");
            DropTable("dbo.Boxes");
            DropTable("dbo.Publishers");
            DropTable("dbo.Topics");
            DropTable("dbo.Lessons");
            DropTable("dbo.Ratios");
            DropTable("dbo.UniversityBranches");
            DropTable("dbo.UniversityBranches_HistoryEducations");
            DropTable("dbo.QuestionAnswerViews");
            DropTable("dbo.Grades");
            DropTable("dbo.GradeLevels");
            DropTable("dbo.EducationYears");
            DropTable("dbo.Exams");
            DropTable("dbo.HistoryEducations");
            DropTable("dbo.EducationSubGroups");
            DropTable("dbo.EducationGroups");
            DropTable("dbo.EducationGroups_Lessons");
            DropTable("dbo.EducationBooks");
            DropTable("dbo.AxillaryBooks");
            DropTable("dbo.Questions");
            DropTable("dbo.QuestionAnswers");
        }
    }
}
