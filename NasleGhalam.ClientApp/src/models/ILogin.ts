export default interface ILogin {
  Username: string;
  Password: string;
}

export const DefaultLogin: ILogin = {
  Username: "",
  Password: ""
};
