namespace NasleGhalam.Common
{
    public enum JwtHashAlgorithm
    {
        RS256,
        HS384,
        HS512
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

    public enum AnswerType : byte
    {
    }

    public enum PaperType : byte
    {
        White = 0,
        Kahi = 1,
        Glasse = 2
    }

    public enum PrintType : byte
    {
        Colered = 0,
        BlackAndWhite = 1
    }

    public enum BookType : byte
    {

    }

    public enum QuestionType : byte
    {
        MultipleChoice = 0,
        Detailed = 1
    }


    public enum TopicHardnessType : byte
    {
        Easy = 0,
        Normal = 1,
        Hard = 2
    }

    public enum QuestionHardnessType : byte
    {
        Easy = 0,
        Normal = 1,
        Hard = 2,
        Olampiad = 3
    }

    public enum AreaType : byte
    {
        Danesh = 0,
        DarkVaFahm = 1,
        Karbord = 2,
        TajzieVaTahlil = 3,
        Tarkib = 4,
        Arzeshyabi = 5
    }


    public enum ReapetnessType : byte
    {
        Low = 0,
        Medium = 1,
        High = 2
    }

    public enum AuthorType : byte
    {
        Konkoor = 0,
        Talif = 1,
        Jozve = 2
    }
}
