import {
  required,
  displayName,
  maxLength,
  numeric,
  requiredDdl
} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IMedia from "src/models/IMedia";

type TMedia = { media: IMedia; validationGroup: string[] };
const mediaValidations: ValidationRuleset<TMedia> = {
  media: {
    Title: {
      displayName: displayName("عنوان"),
      required
    },
    LookupId_MediaType: {
      displayName: displayName("نوع رسانه"),
      numeric,
      requiredDdl: requiredDdl(0)
    },
    
    WriterId: {
      displayName: displayName("نام طراح"),
      requiredDdl: requiredDdl(0)
    },

    Price: {
      displayName: displayName("قیمت"),
      numeric
    },
    
    FileName : {
      displayName: displayName("فایل"),
    },

    CoverImage : {
      displayName: displayName("عکس کاور")
    },

    Length : {
      displayName: displayName("مدت رسانه / تعداد صفحات ")
    },
    
    YearOfBook : {
      displayName: displayName("سال کتاب درسی")
    },

    PagesOfBook : {
      displayName: displayName("صفحه کتاب درسی")
    },

    Description: {
      displayName: displayName("توضیحات"),
      maxLength: maxLength(300)
    },
    
  }
};

export { validationMixin, mediaValidations };
