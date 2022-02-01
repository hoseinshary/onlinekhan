import {
  required,
  displayName,
  maxLength,
  numeric,
  requiredDdl
} from "src/plugins/vuelidate";
import { ValidationRuleset, validationMixin } from "vuelidate";
import IAxillaryBook from "src/models/IAxillaryBook";

type TAxillaryBook = { axillaryBook: IAxillaryBook; validationGroup: string[] };
const axillaryBookValidations: ValidationRuleset<TAxillaryBook> = {
  axillaryBook: {
    Name: {
      displayName: displayName("نام"),
      maxLength: maxLength(50),
      required
    },
    PublishYear: {
      displayName: displayName("سال انتشار"),
      numeric,
      required // todo: add range(1350,1500)
    },
    Author: {
      displayName: displayName("نویسنده"),
      maxLength: maxLength(100),
      required
    },
    Isbn: {
      displayName: displayName("شابک"),
      maxLength: maxLength(100),
      required
    },
    Font: {
      displayName: displayName("قلم"),
      maxLength: maxLength(50)
    },
    LookupId_PrintType: {
      displayName: displayName("نوع چاپ"),
      requiredDdl: requiredDdl(0)
    },
    Price: {
      displayName: displayName("قیمت"),
      numeric,
      required
    },
    OriginalPrice: {
      displayName: displayName("قیمت پشت جلد"),
      numeric,
      required
    },
    LookupId_BookType: {
      displayName: displayName("نوع قطع"),
      requiredDdl: requiredDdl(0)
    },
    LookupId_PaperType: {
      displayName: displayName("نوع کاغذ"),
      requiredDdl: requiredDdl(0)
    },
    Description: {
      displayName: displayName("توضیحات"),
      maxLength: maxLength(300)
    },
    PublisherId: {
      displayName: displayName("انتشارات")
    }
  }
};

export { validationMixin, axillaryBookValidations };
