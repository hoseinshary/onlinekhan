import IQuestion from "./IQuestion";
import IUser, { DefaultUser } from "./IUser";
import { Fonts } from "src/utilities/enumeration";

// interface IAssay {
//   LessonId?: number;
//   LessonName?: string;
//   Checked?: boolean;
//   CountOfEasy?: number;
//   CountOfMedium?: number;
//   CountOfHard?: number;
// }

export class AssayTopic {
  private _checked: boolean;
  get Checked() {
    return this._checked;
  }
  set Checked(value: boolean) {
    this._checked = value;
    if (!value) {
      this.CountOfEasy = 0;
      this.CountOfMedium = 0;
      this.CountOfHard = 0;
    }
  }

  Id: number;
  Name: string;
  ParentTopicId?: number | null;
  LessonId: number;
  CountOfEasy: number;
  CountOfMedium: number;
  CountOfHard: number;
  get CountOfQuestions(): number {
    return this.CountOfEasy + this.CountOfMedium + this.CountOfHard;
  }
  /**
   * constructor
   */
  constructor(
    topicId: number,
    topicName: string,
    lessonId: number,
    parentTopicId?: number | null
  ) {
    this.Id = topicId;
    this.Name = topicName;
    this.ParentTopicId = parentTopicId;
    this.LessonId = lessonId;
    this.Checked = false;
    this.CountOfEasy = 0;
    this.CountOfMedium = 0;
    this.CountOfHard = 0;
  }
}

export class AssayLesson {
  Id: number;
  Name: string;
  Checked: boolean;
  CountOfEasy: number;
  CountOfMedium: number;
  CountOfHard: number;
  LessonOrder: number;
  HaveWhiteSpace: boolean;
  WhiteSpacePosition: boolean;
  get CountOfQuestions(): number {
    return this.CountOfEasy + this.CountOfMedium + this.CountOfHard;
  }
  Topics: Array<AssayTopic>;
  TopicIds : Array<number>;
  Questions: Array<IQuestion>;
  /**
   * constructor
   */
  constructor(lessonId: number, lessonName: string) {
    this.Id = lessonId;
    this.Name = lessonName;
    this.Checked = false;
    this.CountOfEasy = 0;
    this.CountOfMedium = 0;
    this.CountOfHard = 0;
    this.LessonOrder = 0;
    this.Topics = [];
    this.Questions = [];
    this.HaveWhiteSpace = false;
    this.WhiteSpacePosition = false;
    this.TopicIds = [];
  }
}
export  default class AssayCreate {
  Id: number;
  Title: string;
  Time: number;
  LookupId_Importance: number;
  LookupId_Type: number;
  LookupId_QuestionType: number;
  IsPublic: boolean;
  IsOnline: boolean;
  RandomQuestion: boolean;
  RandomOptions: boolean;
  QuestionsRelocation: boolean;
  QuestionsRelocationMode: boolean;
  FontSize: number;
  Font: Fonts;
  TwoPageInOne: boolean;
  NumberOfVarient : number;
  HaveWhiteSpace : boolean;
  Lessons: Array<AssayLesson>;
  Page : number;
  File1: string;
  AssayPdfPath1 : string;
  AssayWordPath1 : string;
  File2: string;
  AssayPdfPath2 : string;
  AssayWordPath2 : string;
  File3: string;
  AssayPdfPath3 : string;
  AssayWordPath3 : string;
  File4: string;
  AssayPdfPath4 : string;
  AssayWordPath4 : string;

  QuestionsPath:Array<string>;

  /**
   * constructor
   */
  constructor() {
    this.Id = 0;
    this.Title = "";
    this.Time = 0;
    this.LookupId_Importance = 0;
    this.LookupId_Type = 0;
    this.LookupId_QuestionType = 0;
    this.IsPublic = false;
    this.IsOnline = false;
    this.RandomOptions = false;
    this.RandomQuestion = false;
    this.QuestionsRelocation = false;
    this.QuestionsRelocationMode = false;
    this.FontSize = 11;
    this.Font = Fonts.BNazanin;
    this.TwoPageInOne = false;
  this.NumberOfVarient = 1;
  this.HaveWhiteSpace = false;
    this.Lessons = [];
    this.Page = 1;
    this.File1 = "";
    this.AssayPdfPath1 = "";
    this.AssayWordPath1 = "";
    this.File2 = "";
    this.AssayPdfPath2 = "";
    this.AssayWordPath2 = "";
    this.File3 = "";
    this.AssayPdfPath3 = "";
    this.AssayWordPath3 = "";
    this.File4 = "";
    this.AssayPdfPath4 = "";
    this.AssayWordPath4 = "";
    this.QuestionsPath = [];
  }
}

export   class AssayNumberOfQuestionReportForTopic {
  LessonReports : Array<LessonReport>
  User : IUser;

  /**
   * constructor
   */
  constructor() {
  this.LessonReports = [];
  this.User = DefaultUser;
  }
}
export   class LessonReport {
  Id: number;
  TopicReports : Array<TopicReport>
  

  /**
   * constructor
   */
  constructor() {
  this.Id  = 0;
  this.TopicReports = [];
  }
}

export   class TopicReport {
  Id: number;
  NumberOfNewQuestions : number;
  NumberOfAssayQuestions : number;
  NumberOfHomeworkQuestions : number;
  

  /**
   * constructor
   */
  constructor() {
  this.Id  = 0;
  this.NumberOfAssayQuestions = 0;
    this.NumberOfHomeworkQuestions = 0;
    this.NumberOfNewQuestions = 0 ;
  }
}

  export   class AssayNumberOfQuestionReport {
    Id: number;
    NumberOfNewQuestions : number;
    NumberOfAssayQuestions : number;
    NumberOfHomeworkQuestions : number;
    User : IUser;
  
    /**
     * constructor
     */
    constructor() {
    this.Id= 0;
    this.NumberOfAssayQuestions = 0;
    this.NumberOfHomeworkQuestions = 0;
    this.NumberOfNewQuestions = 0 ;
    this.User = DefaultUser;
    }



  // constructor(obj?: IAssay) {
  //   this.LessonId = (obj && obj.LessonId) || 0;
  //   this.LessonName = (obj && obj.LessonName) || "";
  //   this.Checked = (obj && obj.Checked) || false;
  //   this.CountOfEasy = (obj && obj.CountOfEasy) || 0;
  //   this.CountOfMedium = (obj && obj.CountOfMedium) || 0;
  //   this.CountOfHard = (obj && obj.CountOfHard) || 0;
  //   this.Topics = [];
  // }

  // constructor(
  //   lessonId: number = 0,
  //   lessonName: string = "",
  //   checked: boolean = false,
  //   countOfEasy: number = 0,
  //   countOfMedium: number = 0,
  //   countOfHard: number = 0
  // ) {
  //   this.LessonId = lessonId;
  //   this.LessonName = lessonName;
  //   this.Checked = checked;
  //   this.CountOfEasy = countOfEasy;
  //   this.CountOfMedium = countOfMedium;
  //   this.CountOfHard = countOfHard;
  // }
}
