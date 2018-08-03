import {
  required,
  displayName,
  maxLength,
  requiredDdl
} from 'plugins/vuelidate';

export default {
  cityObj: {
    Name: {
      displayName: displayName('Name'),
      maxLength: maxLength(50),
      required
    },
    ProvinceId: {
      displayName: displayName('ProvinceId'),
      requiredDdl: requiredDdl(0)
    }
  }
};
