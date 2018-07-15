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
      <br>
      <br>
      <table class="my-table">
        <thead>
          <tr>
            <th>نام</th>
            <th>نام خانوادگی</th>
            <th>سن</th>
            <th>جنسیت</th>
            <th>فرزند</th>
            <th>فعال</th>
            <th>نقش</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="row in allInstance"
              :key="row.id">
            <td>{{row.name}}</td>
            <td>{{row.family}}</td>
            <td>{{row.age}}</td>
            <td>{{row.gender}}</td>
            <td>{{row.children}}</td>
            <td>{{row.isActive}}</td>
            <td>{{row.roleId}}</td>
          </tr>
        </tbody>
      </table>

      <modal-create></modal-create>
    </div>
  </my-panel>
</template>

<script>
import viewModel from 'viewModels/user/userViewModel';
import { mapState } from 'vuex';
import modalCreate from './create';

export default {
  components: {
    'modal-create': modalCreate
  },
  /**
   * data
   */
  data() {
    return {};
  },
  /**
   * methods
   */
  methods: {
    /**
     * open modal create
     */
    showModalCreate() {
      this.$store.commit('grade/openModalCreate');
    },
    /**
     * open modal edit
     */
    showModalEdit() {
      this.isOpenModalEdit = true;
    },
    /**
     * on confirm modal delete cliecked
     */
    confirmEdit(closeModal) {},
    /**
     * open modal delete
     */
    showModalDelete() {
      this.isOpenModalDelete = true;
    },
    /**
     * on confirm modal delete cliecked
     */
    confirmDelete() {
      console.log('delete confirmed');
    },
    btnBack() {}
  },
  validations: viewModel,
  // computed: mapState([
  //   map this.count to store.state.count
  //   'count'
  // ])
  computed: {
    ...mapState({
      allInstance: s => s.grade.allInstance
    })
  }
};
</script>

