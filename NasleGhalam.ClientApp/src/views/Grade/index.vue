<template>

  <my-panel>
    <span slot="title">مقطع تحصیلی</span>
    <div slot="body">
      <!--
      <btn-back @click="btnBack"></btn-back>
      <br>
      <br>
      <br>
      <btn-edit @click="btnBack"></btn-edit>
      <btn-clear @click="btnClear"></btn-clear>
      <btn-back @click="btnBack"></btn-back> -->

      <br>
      <br>
      <br>
      <my-btn-create @click="showModalCreate"></my-btn-create>
      <my-btn-edit @click="showModalEdit"></my-btn-edit>
      <my-btn-delete @click="showModalDelete"></my-btn-delete>
      <my-btn-back @click="btnBack"></my-btn-back>

      <!-- <br>
      <br>
      <br>

      <btn-search @click="showModalDelete"></btn-search>-->

      <my-modal-create :title="modelName"
        size="lg" :openModal="isOpenModalCreate"
        @confirm="confirmCreate"
        @toggle="isOpenModalCreate=$event">

        <my-input :model="$v.name"
          class="col-md-6"></my-input>
        <my-input :model="$v.family"
          class="col-md-6"></my-input>

        <my-field class="col-md-6"
          :model="$v.gender">
          <template slot-scope="data">
            <q-radio v-model="data.obj.$model"
              val="0" label="زن"
            />
            <q-radio v-model="data.obj.$model"
              val="1" label="مرد "
            />
            <q-radio v-model="data.obj.$model"
              val="2" label="دیگر "
            />
          </template>
        </my-field>

        <my-field class="col-md-6"
          :model="$v.children">
          <template slot-scope="data">
            <q-checkbox v-model="data.obj.$model"
              label="دختر" val="0"
            />
            <q-checkbox v-model="data.obj.$model"
              label="پسر " val="1"
            />
          </template>
        </my-field>

        <my-field class="col-md-6"
          :model="$v.isActive">
          <template slot-scope="data">
            <q-toggle v-model="data.obj.$model"
            />
          </template>
        </my-field>

        <!-- <my-field class="col-md-6" :model="$v.roleId">
          <template slot-scope="data">
            <q-select v-model="data.obj.$model" :options="selectOptions" autofocus-filter clearable/>
          </template>
        </my-field> -->

        <!-- <q-field count class="col-md-6">
          <q-select v-model="$v.roleId.$model" :options="selectOptions" clearable autofocus-filter float-label="نقش" />
        </q-field> -->

        <my-select :model="$v.roleId"
          :options="selectOptions"
          class="col-md-6" clearable/>

        <div>{{roleId}}</div>

      </my-modal-create>

      <my-modal-edit :title="modelName"
        size="lg" :openModal="isOpenModalEdit"
        @confirm="confirmEdit"
        @toggle="isOpenModalEdit=$event">

        <my-input :model="$v.name"
          class="col-md-6"></my-input>
        <my-input :model="$v.family"
          class="col-md-6"></my-input>
        <my-input :model="$v.age"
          class="col-md-6"></my-input>

        <my-field class="col-md-6"
          :model="$v.gender">
          <template slot-scope="data">
            <q-radio v-model="data.obj.$model"
              val="0" label="زن"
            />
            <q-radio v-model="data.obj.$model"
              val="1" label="مرد "
            />
            <q-radio v-model="data.obj.$model"
              val="2" label="دیگر "
            />
          </template>
        </my-field>

        <my-field class="col-md-6"
          :model="$v.children">
          <template slot-scope="data">
            <q-checkbox v-model="data.obj.$model"
              label="دختر" val="0"
            />
            <q-checkbox v-model="data.obj.$model"
              label="پسر " val="1"
            />
          </template>
        </my-field>

        <my-field class="col-md-6"
          :model="$v.isActive">
          <template slot-scope="data">
            <q-toggle v-model="data.obj.$model"
            />
          </template>
        </my-field>

        <my-select :model="$v.roleId"
          :options="selectOptions"
          class="col-md-6" clearable/>

      </my-modal-edit>

      <my-modal-delete :title="modelName "
        recordName="علیرضا، اعتمادی"
        :openModal="isOpenModalDelete "
        @confirm="confirmDelete "
        @toggle="isOpenModalDelete=$event "></my-modal-delete>

    </div>
  </my-panel>
</template>

<script>
import viewModel from 'viewModels/user/userViewModel'

export default {
  /**
   * data
   */
  data() {
    return {
      name: '',
      family: '',
      age: '',
      gender: '',
      children: [],
      isActive: false,
      roleId: '',
      selectOptions: [
        {
          label: 'انتخاب کنید',
          value: '0'
        },
        {
          label: 'ادمین',
          value: '1'
        },
        {
          label: 'کاربر ارشد',
          value: '2'
        },
        {
          label: 'کاربر سیستم',
          value: '3'
        }
      ],
      modelName: 'کاربر',
      isOpenModalCreate: false,
      isOpenModalEdit: false,
      isOpenModalDelete: false
    }
  },
  /**
   * methods
   */
  methods: {
    fn(m) {
      console.log(m)
    },
    /**
     * open modal create
     */
    showModalCreate() {
      this.isOpenModalCreate = true
    },
    /**
     * on confirm modal create cliecked
     */
    confirmCreate(closeModal) {
      this.$v.$touch()
      if (closeModal) {
        this.isOpenModalCreate = false
      }
    },
    /**
     * open modal edit
     */
    showModalEdit() {
      this.isOpenModalEdit = true
    },
    /**
     * on confirm modal delete cliecked
     */
    confirmEdit(closeModal) {},
    /**
     * open modal delete
     */
    showModalDelete() {
      this.isOpenModalDelete = true
    },
    /**
     * on confirm modal delete cliecked
     */
    confirmDelete() {
      console.log('delete confirmed')
    },
    btnBack() {}
  },
  validations: viewModel
}
</script>

