// import something here
import "src/css/site.css";
// import 'utilities/extensions';

/** notification */
import Snotify from "vue-snotify";
import "vue-snotify/styles/material.css";

/** panel */
import BasePanel from "src/Components/BasePanel.vue";

/** hr */
import BaseHr from "src/Components/BaseHr.vue";

/** table */
import BaseTable from "src/Components/BaseTable/index.vue";

/* btn */
import BtnSave from "src/Components/Buttons/BaseBtnSave.vue";
import BtnSaveBack from "src/Components/Buttons/BaseBtnSaveBack.vue";
import BtnClear from "src/Components/Buttons/BaseBtnClear.vue";
import BtnBack from "src/Components/Buttons/BaseBtnBack.vue";
import BtnEdit from "src/Components/Buttons/BaseBtnEdit.vue";
import BtnDelete from "src/Components/Buttons/BaseBtnDelete.vue";
import BtnCreate from "src/Components/Buttons/BaseBtnCreate.vue";
import BtnSearch from "src/Components/Buttons/BaseBtnSearch.vue";

/* modal */
import BsModal from "src/Components/Modals/BsModal.vue";
import ModalCreate from "src/Components/Modals/BaseModalCreate.vue";
import ModalEdit from "src/Components/Modals/BaseModalEdit.vue";
import ModalDelete from "src/Components/Modals/BaseModalDelete.vue";

/* FormComponents */
import BaseField from "src/Components/Forms/BaseField.vue";
import BaseInput from "src/Components/Forms/BaseInput.vue";
import BaseSelect from "src/Components/Forms/BaseSelect.vue";
import BaseDatePicker from "src/Components/Forms/BaseDatePicker.vue";

/* util js file */
import util from "src/utilities";

/* datetime picker */
// https://talkhabi.github.io/vue-persian-datetime-picker
import VuePersianDatetimePicker from "vue-persian-datetime-picker";

// leave the export, even if you don't use it
export default ({ app, router, Vue }) => {
  // register notification
  Vue.use(Snotify);

  // register panel
  Vue.component("base-panel", BasePanel);

  // register hr
  Vue.component("base-hr", BaseHr);

  // register table
  Vue.component("base-table", BaseTable);

  // register btn
  Vue.component("base-btn-save", BtnSave);
  Vue.component("base-btn-save-back", BtnSaveBack);
  Vue.component("base-btn-clear", BtnClear);
  Vue.component("base-btn-back", BtnBack);
  Vue.component("base-btn-edit", BtnEdit);
  Vue.component("base-btn-delete", BtnDelete);
  Vue.component("base-btn-create", BtnCreate);
  Vue.component("base-btn-search", BtnSearch);

  // register modal
  Vue.component("bs-modal", BsModal);
  Vue.component("base-modal-create", ModalCreate);
  Vue.component("base-modal-edit", ModalEdit);
  Vue.component("base-modal-delete", ModalDelete);

  // register input
  Vue.component("base-field", BaseField);
  Vue.component("base-input", BaseInput);
  Vue.component("base-select", BaseSelect);
  Vue.component("base-date-picker", BaseDatePicker);

  /* datetime picker */
  Vue.component("date-picker", VuePersianDatetimePicker);

  // register this.util
  Object.defineProperty(Vue.prototype, "$util", {
    value: util
  });
};
