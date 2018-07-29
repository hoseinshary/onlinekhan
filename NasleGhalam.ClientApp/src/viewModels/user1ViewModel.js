import {
  required,
  maxLength,
  minLength,
  numeric,
  displayName,
  requiredDdl,
  onlyPersianChar,
  notPersianChar
} from 'plugins/vuelidate';

export default {
  instance: {
    name: {
      displayName: displayName('نام'),
      required,
      maxLength: maxLength(50),
      onlyPersianChar: onlyPersianChar()
    },
    family: {
      displayName: displayName('نام خانوادگی'),
      onlyPersianChar: onlyPersianChar(),
      required,
      maxLength: maxLength(50)
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
};
