import {
  required,
  displayName,
  maxLength,
  minLength,
  requiredDdl,
  numeric,
  onlyPersianChar,
  notPersianChar
} from 'plugins/vuelidate';

export default {
  userObj: {
    Name: {
      displayName: displayName('نام'),
      maxLength: maxLength(50),
      required,
      onlyPersianChar: onlyPersianChar()
    },
    Family: {
      displayName: displayName('نام خانوادگی'),
      maxLength: maxLength(50),
      required,
      onlyPersianChar: onlyPersianChar()
    },
    Username: {
      displayName: displayName('نام کاربری'),
      maxLength: maxLength(50),
      required
    },
    Password: {
      displayName: displayName('رمز عبور'),
      maxLength: maxLength(50),
      minLength: minLength(5),
      required,
      notPersianChar: notPersianChar()
    },
    IsActive: {
      displayName: displayName('وضعیت'),
      required
    },
    NationalNo: {
      displayName: displayName('کد ملی'),
      maxLength: maxLength(10),
      required,
      numeric
    },
    Gender: {
      displayName: displayName('جنسیت'),
      required
    },
    Phone: {
      displayName: displayName('تلفن ثابت'),
      maxLength: maxLength(8),
      minLength: minLength(8),
      required,
      numeric
    },
    Mobile: {
      displayName: displayName('موبایل'),
      maxLength: maxLength(10),
      minLength: minLength(10),
      required,
      numeric
    },
    RoleId: {
      displayName: displayName('نقش'),
      requiredDdl: requiredDdl(0)
    },
    ProvinceId: {
      displayName: displayName('استان'),
      requiredDdl: requiredDdl(0)
    },
    CityId: {
      displayName: displayName('شهر'),
      requiredDdl: requiredDdl(0)
    }
  }
};
