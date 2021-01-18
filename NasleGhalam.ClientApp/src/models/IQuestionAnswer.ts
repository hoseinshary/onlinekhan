import IWriter from "./IWriter";

export default interface IQuestionAnswer {
  Id: number;
  Title: string;
  FilePath: string;
  Context: string;
  LookupId_AnswerType: number;
  Description: string;
  LessonName: string;
  QuestionAnswerPicturePath: string;
  WriterId: number;
  IsMaster: boolean;
  IsActive: boolean;
  IsDelete: boolean;
  IsUpdate: boolean;
  QuestionId: number;
  Writer?: IWriter;
}

export const DefaultQuestionAnswer: IQuestionAnswer = {
  Id: 0,
  Title: "",
  FilePath: "",
  Context: "",
  LookupId_AnswerType: 0,
  LessonName: "",
  Description: "",
  QuestionAnswerPicturePath: "",
  WriterId: 0,
  IsMaster: false,
  IsActive: false,
  IsDelete: false,
  IsUpdate: false,
  QuestionId: 0
};
