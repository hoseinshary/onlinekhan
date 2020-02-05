import IUser, { DefaultUser } from "./IUser";

export default interface ITeacher {
  Id: number;
  FatherName: string;
  Address: string;
  User: IUser;
}

export const DefaultTeacher: ITeacher = {
  Id: 0,
  FatherName: "",
  Address: "",
  User: DefaultUser
};
