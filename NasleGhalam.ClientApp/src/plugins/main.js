// import something here
import '../css/my-style.css';
// import 'utilities/extentions';

/** notification */
import Snotify from 'vue-snotify';
import 'vue-snotify/styles/material.css';

/** panel */
import myPanel from 'components/my-panel';

/** hr */
import myHr from 'components/my-hr';

/** table */
import myTable from 'components/my-table';

/* btn */
import btnSave from 'components/buttons/my-btn-save';
import btnSaveBack from 'components/buttons/my-btn-save-back';
import btnClear from 'components/buttons/my-btn-clear';
import btnBack from 'components/buttons/my-btn-back';
import btnEdit from 'components/buttons/my-btn-edit';
import btnDelete from 'components/buttons/my-btn-delete';
import btnCreate from 'components/buttons/my-btn-create';
import btnSearch from 'components/buttons/my-btn-search';

/* modal */
import bsModal from 'components/modals/bs-modal';
import modalCreate from 'components/modals/my-modal-create';
import modalEdit from 'components/modals/my-modal-edit';
import modalDelete from 'components/modals/my-modal-delete';

/* form-components */
import myField from 'components/form-components/my-field';
import myInput from 'components/form-components/my-input';
import mySelect from 'components/form-components/my-select';
import myUploader from 'components/form-components/my-uploader';

/* util js file */
import util from 'utilities/util';

// leave the export, even if you don't use it
export default ({ app, router, Vue }) => {
  // register notification
  Vue.use(Snotify);

  // register panel
  Vue.component('my-panel', myPanel);

  // register hr
  Vue.component('my-hr', myHr);

  // register table
  Vue.component('my-table', myTable);

  // register btn
  Vue.component('my-btn-save', btnSave);
  Vue.component('my-btn-save-back', btnSaveBack);
  Vue.component('my-btn-clear', btnClear);
  Vue.component('my-btn-back', btnBack);
  Vue.component('my-btn-edit', btnEdit);
  Vue.component('my-btn-delete', btnDelete);
  Vue.component('my-btn-create', btnCreate);
  Vue.component('my-btn-search', btnSearch);

  // register modal
  Vue.component('bs-modal', bsModal);
  Vue.component('my-modal-create', modalCreate);
  Vue.component('my-modal-edit', modalEdit);
  Vue.component('my-modal-delete', modalDelete);

  // register input
  Vue.component('my-field', myField);
  Vue.component('my-input', myInput);
  Vue.component('my-select', mySelect);
  Vue.component('my-uploader', myUploader);

  // register this.util
  Object.defineProperty(Vue.prototype, '$util', {
    value: util
  });
};
