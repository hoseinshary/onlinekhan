enum CrudType {
  Create,
  Update,
  Delete,
  Read
}

enum ModalType {
  Create,
  Update,
  Delete,
  Read
}

enum MessageType {
  Error = 0,
  Success = 1,
  Warning = 2,
  NotFound = 3,
  Unauthorized = 4
}

export { CrudType, ModalType, MessageType };
