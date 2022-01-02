import ITopic from "./ITopic";

export default interface IEducationBook {
  Id: number;
  Name: string;
  PublishYear: number | string;
  IsExamSource: boolean;
  IsActive: boolean;
  IsChanged: boolean;
  LessonId: number;
  TopicIds: Array<number>;
  Topics: Array<ITopic>;
}

export const DefaultEducationBook: IEducationBook = {
  Id: 0,
  Name: "",
  PublishYear: "",
  IsExamSource: false,
  IsActive: false,
  IsChanged: false,
  LessonId: 0,
  TopicIds: [],
  Topics: []
};
