import { UserType } from "src/utilities/enumeration";

export default interface IRole {
  Id: number;
  Name: string;
  Level: number | string;
  UserType: UserType | string;
}

export const DefaultRole: IRole = {
  Id: 0,
  Name: "",
  Level: "",
  UserType: ""
};
