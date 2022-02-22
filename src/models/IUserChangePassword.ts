export default interface IUserChangePassword {
  
  OldPassword: string;
  NewPassword: string;
  ReNewPassword: string;
}

export const DefaultUserChangePassword: IUserChangePassword = {
  OldPassword: "",
  NewPassword: "",
  ReNewPassword: ""
};
