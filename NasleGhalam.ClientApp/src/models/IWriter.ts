export default interface IWriter {
  Id: number;
  Name: string;
  UserId?: number | null;
}

export const DefaultWriter: IWriter = {
  Id: 0,
  Name: "",
  UserId: null
};
