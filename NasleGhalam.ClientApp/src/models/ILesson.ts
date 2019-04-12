import IEducationTree from "./IEducationTree";
import IRatio from "./IRatio";

export default interface ILesson {
  Id: number;
  Name: string;
  IsMain: boolean;
  LookupId_Nezam: number;
  EducationTreeIds: Array<number>;
  EducationTrees?: Array<IEducationTree>;
  Ratios: Array<IRatio>;
}

export const DefaultLesson: ILesson = {
  Id: 0,
  Name: "",
  IsMain: false,
  LookupId_Nezam: 0,
  EducationTreeIds: [],
  Ratios: []
};
