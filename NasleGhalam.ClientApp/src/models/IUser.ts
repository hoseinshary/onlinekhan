export default interface IUser {
  Id: number;
  Name: string;
  Family: string;
  Username: string;
  Password: string;
  IsActive: boolean;
  NationalNo: string;
  Gender: boolean | string;
  Phone: string;
  Mobile: string;
  RoleId: number;
  CityId: number;
  ProvinceId: number;
}

export const DefaultUser: IUser = {
  Id: 0,
  Name: "",
  Family: "",
  Username: "",
  Password: "",
  IsActive: true,
  NationalNo: "",
  Gender: "",
  Phone: "",
  Mobile: "",
  RoleId: 0,
  CityId: 0,
  ProvinceId: 0
};
