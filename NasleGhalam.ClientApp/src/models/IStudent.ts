import IUser, { DefaultUser } from "./IUser";

export default interface IStudent {
  Id: number;
  FatherName: string;
  Address: string;
  User: IUser;
}

export const DefaultStudent: IStudent = {
  Id: 0,
  FatherName: "",
  Address: "",
  User: DefaultUser
};
