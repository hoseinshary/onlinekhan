import IUser, { DefaultUser } from "./IUser";
import {Field, Maghta } from "../utilities/enumeration";

export default interface IStudent {
  Id: number;
  FatherName: string;
  Address: string;
  User: IUser;
  IntroducedCode:string;
  EducationGroupEnum:Field;
  GradeEnum:Maghta;
  SchoolName:string;
}

export const DefaultStudent: IStudent = {
  Id: 0,
  FatherName: "",
  Address: "",
  User: DefaultUser,
  IntroducedCode:"",
  EducationGroupEnum:0,
  GradeEnum:0,
  SchoolName:"",
};
