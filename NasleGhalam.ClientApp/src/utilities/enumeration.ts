enum UserType {
  Organ = 0,
  Student = 1,
  Teacher = 2
}

enum MessageType {
  Error = 0,
  Success = 1,
  Warning = 2,
  NotFound = 3,
  Unauthorized = 4
}

enum EducationTreeState {
  Grade = 1,
  SubTree = 2,
  EducationGroup = 3,
  GradeLevel = 4
}

enum Fonts {
  BNazanin = 0,
  Arial = 1
}

export { UserType, MessageType, EducationTreeState, Fonts };
