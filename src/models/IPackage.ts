import ILesson from "./ILesson";
export default interface IPackage {
  Id: number;
  Name: string;
  ImageFile: string;
  IsActive: boolean;
  Price: number;
  TimeDays: number;
  Description: string;
  Lessons: Array<ILesson>;
  LessonIds?: Array<number>;
}

export const DefaultPackage: IPackage = {
  Id: 0,
  Name: "",
  ImageFile: "",
  IsActive: false,
  Price: 0,
  TimeDays: 0,
  Description: "",
  Lessons: [],
  LessonIds: []
};
