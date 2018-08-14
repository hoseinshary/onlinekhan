import {
  required,
  displayName,
  maxLength,
  requiredDdl
} from 'plugins/vuelidate';

export default {
  cityObj: {
    Name: {
      displayName: displayName('نام'),
      maxLength: maxLength(50),
      required
    },
    ProvinceId: {
      displayName: displayName('استان'),
      requiredDdl: requiredDdl(0)
    }
  }
};
