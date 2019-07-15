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
    if (value) {
      this.CountOfEasy = 0;
      this.CountOfMedium = 0;
      this.CountOfHard = 0;
    }
  }

  Id: number;
  Name: string;
  label: string;
  ParentTopicId?: number | null;
  LessonId: number;
  header: string;
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
    this.label = topicName;
    this.ParentTopicId = parentTopicId;
    this.LessonId = lessonId;
    this.header = "custom";
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
  get CountOfQuestions(): number {
    return this.CountOfEasy + this.CountOfMedium + this.CountOfHard;
  }
  Topics: Array<AssayTopic>;
  TopicsTree: Array<AssayTopic>;
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
    this.Topics = [];
    this.TopicsTree = [];
  }
}

export default class AssayCreate {
  Title: string;
  Time: number;
  LookupId_Importance: number;
  LookupId_Type: number;
  LookupId_QuestionType: number;
  IsPublic: boolean;
  IsOnline: boolean;
  RandomQuestion: boolean;
  RandomOptions: boolean;
  Lessons: Array<AssayLesson>;

  /**
   * constructor
   */
  constructor() {
    this.Title = "";
    this.Time = 0;
    this.LookupId_Importance = 0;
    this.LookupId_Type = 0;
    this.LookupId_QuestionType = 0;
    this.IsPublic = false;
    this.IsOnline = false;
    this.RandomOptions = false;
    this.RandomQuestion = false;
    this.Lessons = [];
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
