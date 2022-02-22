export default interface IPublisher {
  Id: number;
  Name: string;
}

export const DefaultPublisher: IPublisher = {
  Id: 0,
  Name: ""
};
