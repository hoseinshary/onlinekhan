<template>
  <my-modal-edit :title="modelName"
                 :show="isOpenModalEdit"
                 @confirm="submitEditStore"
                 @reset="resetEditStore"
                 @close="toggleModalEditStore(false)">

    <my-input :model="$v.userObj.Name" class="col-md-6" />
          
          <my-input :model="$v.userObj.Family" class="col-md-6" />
          
          <my-input :model="$v.userObj.Username" class="col-md-6" />
          
          <my-input :model="$v.userObj.Password" class="col-md-6" />
          
          <my-field class="col-md-6" :model="$v.userObj.IsActive">
              <template slot-scope="data">
                <q-radio v-model="data.obj.$model" val="false" label="false" />
                <q-radio v-model="data.obj.$model" val="true" label="true" />
              </template>
            </my-field>
            
            <my-input :model="$v.userObj.NationalNo" class="col-md-6" />
          
          <my-field class="col-md-6" :model="$v.userObj.Gender">
              <template slot-scope="data">
                <q-radio v-model="data.obj.$model" val="false" label="false" />
                <q-radio v-model="data.obj.$model" val="true" label="true" />
              </template>
            </my-field>
            
            <my-input :model="$v.userObj.Phone" class="col-md-6" />
          
          <my-input :model="$v.userObj.Mobile" class="col-md-6" />
          
          <my-select :model="$v.userObj.RoleId" :options="" class="col-md-6" clearable />
          
          <my-select :model="$v.userObj.CityId" :options="" class="col-md-6" clearable />
          
          

  </my-modal-edit>
</template>

<script>
import viewModel from 'viewModels/userViewModel';
import { mapState, mapActions } from 'vuex';

export default {
  /**
   * methods
   */
  methods: {
    ...mapActions('userStore', [
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
    ...mapState('userStore', {
      modelName: 'modelName',
      userObj: 'userObj',
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

