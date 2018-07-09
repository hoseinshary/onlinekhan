import {
  required,
  maxLength,
  minLength,
  numeric,
  displayName,
  requiredDdl,
  onlyPersianChar,
  notPersianChar
} from 'plugins/vuelidate'

export default {
  name: {
    displayName: displayName('نام'),
    required,
    minLength: minLength(4),
    onlyPersianChar: onlyPersianChar()
  },
  family: {
    displayName: displayName('نام خانوادگی'),
    notPersianChar: notPersianChar(),
    required,
    // minLength: minLength(4),
    maxLength: maxLength(20)
  },
  gender: {
    displayName: displayName('جنسیت'),
    required
  },
  age: {
    displayName: displayName('سن'),
    required,
    numeric
  },
  children: {
    displayName: displayName('فرزند'),
    required
  },
  isActive: {
    displayName: displayName('فعال'),
    required
  },
  roleId: {
    displayName: displayName('نقش'),
    required,
    requiredDdl: requiredDdl(0)
  }
}
