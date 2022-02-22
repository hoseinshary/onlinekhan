import IEducationTree from "./IEducationTree";

export default interface IEducationSubGroup {
  Id: number;
  Name: string;
  EducationTreeId: number;
  EducationTree?: IEducationTree;
}

export const DefaultEducationSubGroup: IEducationSubGroup = {
  Id: 0,
  Name: "",
  EducationTreeId: 0
};
