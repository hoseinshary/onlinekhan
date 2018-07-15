<template>
  <my-modal-create :title="modelName"
                   size="lg"
                   :openModal="isOpenModalCreate"
                   @confirm="confirmCreate"
                   @toggle="toggle($event)">

    <my-input :model="$v.instance.name"
              class="col-md-6">
    </my-input>

    <my-input :model="$v.instance.family"
              class="col-md-6">
    </my-input>

    <my-field class="col-md-6"
              :model="$v.instance.gender">
      <template slot-scope="data">
        <q-radio v-model="data.obj.$model"
                 val="0"
                 label="زن" />
        <q-radio v-model="data.obj.$model"
                 val="1"
                 label="مرد " />
        <q-radio v-model="data.obj.$model"
                 val="2"
                 label="دیگر " />
      </template>
    </my-field>

    <my-field class="col-md-6"
              :model="$v.instance.children">
      <template slot-scope="data">
        <q-checkbox v-model="data.obj.$model"
                    label="دختر"
                    val="0" />
        <q-checkbox v-model="data.obj.$model"
                    label="پسر "
                    val="1" />
      </template>
    </my-field>

    <my-field class="col-md-6"
              :model="$v.instance.isActive">
      <template slot-scope="data">
        <q-toggle v-model="data.obj.$model" />
      </template>
    </my-field>

    <my-select :model="$v.instance.roleId"
               :options="selectOptions"
               class="col-md-6"
               clearable />

  </my-modal-create>
</template>

<script>
import viewModel from 'viewModels/user/userViewModel'
import { mapState } from 'vuex'

export default {
  /**
   * data
   */
  data() {
    return {
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
      ]
    }
  },
  /**
   * methods
   */
  methods: {
    // increment() {
    //   this.$store.dispatch('grade/increment')
    // },
    // decrement() {
    //   this.$store.commit('gradelevel/decrement', 3)
    // },
    toggle(b) {
      if (b) {
        this.$store.commit('grade/openModalCreate')
      } else {
        this.$store.commit('grade/closeModalCreate')
      }
    },
    confirmCreate(closeModal) {
      if (!this.$v.$invalid) {
        console.log('invalide ')
        return
      }
      this.$store.commit('grade/create')

      if (closeModal) {
        this.$store.commit('grade/closeModalCreate')
      }
    }
  },
  validations: viewModel,
  computed: {
    ...mapState({
      modelName: s => s.grade.modelName,
      isOpenModalCreate: s => s.grade.isOpenModalCreate,
      instance: s => s.grade.instance
    })
  },
  validations: viewModel
}
</script>

