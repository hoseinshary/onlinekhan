import { UserType } from "src/utilities/enumeration";

export default interface IRole {
  Id: number;
  Name: string;
  Level: number;
  UserType?: UserType;
}

export const DefaultRole: IRole = {
  Id: 0,
  Name: "",
  Level: 0
};
