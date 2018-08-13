import {
  required,
  displayName,
  maxLength,
  minLength,
  numeric,
  requiredDdl
} from 'plugins/vuelidate';

export default {
  userObj: {
    Name: {
      displayName: displayName('نام'),
      maxLength: maxLength(50),
      required
    },
    Family: {
      displayName: displayName('نام خانوادگی'),
      maxLength: maxLength(50),
      required
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
      required
    },
    IsActive: {
      displayName: displayName('فعال'),
      required
    },
    NationalNo: {
      displayName: displayName('کد ملی'),
      maxLength: maxLength(10),
      required
    },
    Gender: {
      displayName: displayName('جنسیت'),
      required
    },
    Phone: {
      displayName: displayName('تلفن ثابت"'),
      maxLength: maxLength(8),
      minLength: minLength(8),
      required
    },
    Mobile: {
      displayName: displayName('Mobile'),
      maxLength: maxLength(10),
      minLength: minLength(10),
      required
    },
    RoleId: {
      displayName: displayName('RoleId'),
      requiredDdl: requiredDdl(0)
    },
    CityId: {
      displayName: displayName('CityId'),
      requiredDdl: requiredDdl(0)
    }
  }
};
