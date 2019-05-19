import IEducationTree from "./IEducationTree";
import IRatio from "./IRatio";

export default interface ILesson {
  Id: number;
  Name: string;
  IsMain: boolean;
  NumberOfJudges: number;
  LookupId_Nezam: number;
  EducationTreeIds: Array<number>;
  EducationTrees?: Array<IEducationTree>;
  Ratios: Array<IRatio>;
  Checked: boolean;
}

export const DefaultLesson: ILesson = {
  Id: 0,
  Name: "",
  IsMain: false,
  NumberOfJudges: 1,
  LookupId_Nezam: 0,
  EducationTreeIds: [],
  Ratios: [],
  Checked: false
};
