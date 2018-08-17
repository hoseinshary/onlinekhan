<template>
  <my-modal-create :title="modelName"
                   :show="isOpenModalCreate"
                   @confirm="submitCreateStore"
                   @reset="resetCreateStore"
                   @close="toggleModalCreateStore(false)"
                   @open="getAllEduGroupAndEduSubGroupStore()">

    <my-input :model="$v.instanceObj.Name"
              class="col-md-12" />
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

  </my-modal-create>
</template>

<script>
import viewModel from 'viewModels/lessonViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  data() {
    return {};
  },
  /**
   * methods
   */
  methods: {
    ...mapActions('lessonStore', [
      'toggleModalCreateStore',
      'createVueStore',
      'submitCreateStore',
      'resetCreateStore',
      'getAllEduGroupAndEduSubGroupStore'
    ])
  },
  /**
   * computed
   */
  computed: {
    ...mapState('lessonStore', {
      modelName: 'modelName',
      instanceObj: 'instanceObj',
      isOpenModalCreate: 'isOpenModalCreate',
      eduGroupAndEduSubGroupLst: 'eduGroupAndEduSubGroupLst'
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
    this.createVueStore(this);
  }
};
</script>

<style scoped>
div >>> .inline {
  display: inline-flex;
}
</style>
