import ITeacher, { DefaultTeacher } from "./ITeacher";

export default interface ITeacherGroup {
  Id: number;
  Name: string;
  TeacherId : number,
  Students:Array<number>,
  Teacher : ITeacher
}
export interface IstudentGroup {
  Id: number,
  FullName: string,
  NationalNo : string,
  Checked : boolean
}


export const DefaultTeacherGroup: ITeacherGroup = {
  Id: 0,
  Name: "",
  TeacherId : 0,
  Students:[],
  Teacher : DefaultTeacher
};

export const DefaultIstudentGroup : IstudentGroup = {
  Id: 0,
  FullName: '',
  NationalNo : '',
  Checked : false
}

