using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.Common
{
    public enum JwtHashAlgorithm
    {
        RS256,
        HS384,
        HS512
    }

    public enum Fonts
    {
        BNazanin = 0,
        Arial = 1,
    }

    public enum KindRequest
    {
        testi = 0,
        tashrihi = 1,
        both = 2
    }

    

    public enum Maghta
    {
        dah = 0,
        yazdah = 1,
        davazdah =2 ,
        konkoor = 3,
    }

    public enum TypeEducationCenter
    {
        dolati = 0,
        azad = 1,
        gheyreEntefaie = 2,
        payamnoor = 3,
        beynolmelal =4,
        others=5
    }

    public enum Degree
    {
        zireDiplom = 0,
        diplom = 1,
        kardani = 2 ,
        karshenasi = 3,
        karshenasiarshad = 4,
        doktora = 5

    }

    public enum DegreeCertificate
    {
        daneshamooz = 0,
        diplom = 1,
        kardaniStudent = 2,
        kardani = 3,
        karshenasiStudent = 4,
        karshenasi = 5,
        karshenasiarshadStudent = 6,
        karshenasiarshad = 7,
        doktoraStudent = 8,
        doktora = 9

    }

    public enum MessageType
    {
        Error = 0,
        Success = 1,
        Warning = 2,
        NotFound = 3,
        Unauthorized = 4
    }

    public enum CrudType
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }

    public enum EqualType : byte
    {
        Equal = 0,
        Same = 1
    }

    public enum UserType
    {
        [Display(Name = "سازمانی")]
        Organ = 0,

        [Display(Name = "دانش آموز")]
        Student = 1,

        [Display(Name = "معلم")]
        Teacher = 2
    }

    public enum EducationTreeState
    {
        Grade = 1,
        SubTree = 2,
        EducationGroup = 3,
        GradeLevel = 4,
    }

    //public enum AnswerType : byte
    //{
    //}

    //public enum PaperType : byte
    //{
    //    White = 0,
    //    Kahi = 1,
    //    Glasse = 2
    //}

    //public enum PrintType : byte
    //{
    //    Colered = 0,
    //    BlackAndWhite = 1
    //}

    //public enum BookType : byte
    //{

    //}

    //public enum QuestionType : byte
    //{
    //    MultipleChoice = 0,
    //    Detailed = 1
    //}


    //public enum TopicHardnessType : byte
    //{
    //    Easy = 0,
    //    Normal = 1,
    //    Hard = 2
    //}

    //public enum QuestionHardnessType : byte
    //{
    //    Easy = 0,
    //    Normal = 1,
    //    Hard = 2,
    //    Olampiad = 3
    //}

    //public enum AreaType : byte
    //{
    //    Danesh = 0,
    //    DarkVaFahm = 1,
    //    Karbord = 2,
    //    TajzieVaTahlil = 3,
    //    Tarkib = 4,
    //    Arzeshyabi = 5
    //}


    //public enum ReapetnessType : byte
    //{
    //    Low = 0,
    //    Medium = 1,
    //    High = 2P
    //}

    //public enum AuthorType : byte
    //{
    //    Konkoor = 0,
    //    Talif = 1,
    //    Jozve = 2
    //}
}
