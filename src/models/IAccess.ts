export default interface IRole {
  ModuleId: number;
  ControllerId: number;
}

export const DefaultAccess: IRole = {
  ModuleId: 0,
  ControllerId: 0
};
