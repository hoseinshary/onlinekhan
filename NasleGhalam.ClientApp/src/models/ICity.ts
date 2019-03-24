import IProvince, { DefaultProvince } from "./IProvince";

export default interface ICity {
  Id: number;
  Name: string;
  Province?: IProvince;
}

export const DefaultCity: ICity = {
  Id: 0,
  Name: "",
  Province: DefaultProvince
};
