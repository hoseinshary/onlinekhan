<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @close="toggleModalEditStore(false)">

    <my-input :model="$v.instanceObj.Name"
              class="col-md-6" />
    <my-field class="col-md-6"
              :model="$v.instanceObj.IsMain">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 val="false"
                 label="خیر" />
        <q-radio v-model="data.obj.$model"
                 val="true"
                 label="بلی" />
      </template>
    </my-field>
    <fieldset class="col-12">
      <legend>گروههای آموزشی</legend>
      <div class="row"
           v-if="eduGroupAndEduSubGroupLst">

        <div class="col-sm-2"
             v-for="group in eduGroupAndEduSubGroupLst"
             :key="group.Name">
          <q-checkbox v-model="group.IsChecked"
                      :label="group.Name" />
        </div>
      </div>
    </fieldset>

    <div class="col-12"
         v-for="group in eduGroupAndEduSubGroupLst.filter(x => x.IsChecked)"
         :key="group.Id">
      <q-slide-transition>

        <fieldset class="col-12">
          <legend>گروههای آموزشی</legend>
          <div class="inline col-sm-4"
               style="padding-top:5px;"
               v-for="subGroup in group.SubGroups"
               :key="subGroup.EducationSubGroupId">
            <div style="margin-top:20px;">{{subGroup.Name}}:</div>
            <q-input type="number"
                     style="margin-right:10px; width:20%;"
                     v-model="subGroup.Ratio"
                     float-label='ضریب'></q-input>
          </div>
        </fieldset>
      </q-slide-transition>

    </div>

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/lessonViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('lessonStore', [
      'toggleModalEditStore',
      'editVueStore',
      'submitEditStore',
      'resetEditStore'
    ])
  },
  /**
   * computed
   */
  computed: {
    ...mapState('lessonStore', {
      modelName: 'modelName',
      instanceObj: 'instanceObj',
      isOpenModalEdit: 'isOpenModalEdit'
    })
  },
  /**
   * validations
   */
  validations: viewModel,
  /**
   * created
   */
  created() {
    this.editVueStore(this);
  }
};
</script>

