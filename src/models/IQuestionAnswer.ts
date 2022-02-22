import IWriter from "./IWriter";

import {
  QuestionAnswerType
} from "src/utilities/enumeration";

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
  QuestionAnswerType : QuestionAnswerType;
  QuestionAnswerTypeDisplay : string;
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
  QuestionId: 0,
  QuestionAnswerType: QuestionAnswerType["نا مشخص"],
  QuestionAnswerTypeDisplay : ""

};
