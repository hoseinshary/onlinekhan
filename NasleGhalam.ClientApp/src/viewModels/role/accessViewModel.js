import { requiredDdl, displayName } from 'plugins/vuelidate';

export default {
  accessObj: {
    ModuleId: {
      displayName: displayName('منو'),
      requiredDdl: requiredDdl(0)
    },
    ControllerId: {
      displayName: displayName('زیر منو'),
      requiredDdl: requiredDdl(0)
    }
  }
};
