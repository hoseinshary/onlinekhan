// interface IAssay {
//   LessonId?: number;
//   LessonName?: string;
//   Checked?: boolean;
//   CountOfEasy?: number;
//   CountOfMedium?: number;
//   CountOfHard?: number;
// }

class AssayTopic {
  Id: number;
  label: string;
  ParentTopicId: number;
  lessonId: number;
  header: string;
  CountOfEasy: number;
  CountOfMedium: number;
  CountOfHard: number;
  get CountOfQuestions() {
    return this.CountOfEasy + this.CountOfMedium + this.CountOfHard;
  }
}

export class AssayLesson {
  Id: number;
  Name: string;
  Checked: boolean;
  CountOfEasy: number;
  CountOfMedium: number;
  CountOfHard: number;
  get CountOfQuestions() {
    return this.CountOfEasy + this.CountOfMedium + this.CountOfHard;
  }
  Topics: Array<AssayTopic>;
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
  }
}

export default class AssayCreate {
  Lessons: Array<AssayLesson> = [];

  /**
   * constructor
   */
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
