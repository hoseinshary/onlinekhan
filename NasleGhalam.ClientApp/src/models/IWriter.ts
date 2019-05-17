export default interface IWriter {
  Id: number;
  Name: string;
}

export const DefaultWriter: IWriter = {
  Id: 0,
  Name: ""
};
