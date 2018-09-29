import { displayName, requiredDdl } from 'plugins/vuelidate';

export default {
  universityBranchIndexObj: {
    EducationGroupId: {
      displayName: displayName('گروه آموزشی'),
      requiredDdl: requiredDdl(0)
    }
  }
};
