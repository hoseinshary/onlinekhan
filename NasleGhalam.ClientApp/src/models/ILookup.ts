export default interface ILookup {
  Id: number;
  Name: string;
  Value: string;
  State: number;
}

export const DefaultLookup: ILookup = {
  Id: 0,
  Name: "",
  Value: "",
  State: 0
};
