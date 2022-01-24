import IStudent from "./IStudent";
import ITeacher, { DefaultTeacher } from "./ITeacher";

export default interface ITeacherGroup {
  Id: number;
  Name: string;
  TeacherId : number,
  StudentsId:Array<number>,
  Students:Array<IStudent>,
  StudentsName:Array<String>,
  Teacher : ITeacher
}



export const DefaultTeacherGroup: ITeacherGroup = {
  Id: 0,
  Name: "",
  TeacherId : 0,
  StudentsId:[],
  Students : [],
  StudentsName:[],
  Teacher : DefaultTeacher
};




export interface IstudentGroup {
  Id: number,
  FullName: string,
  NationalNo : string,
  Checked : boolean
}
export const DefaultIstudentGroup : IstudentGroup = {
  Id: 0,
  FullName: '',
  NationalNo : '',
  Checked : false
}

