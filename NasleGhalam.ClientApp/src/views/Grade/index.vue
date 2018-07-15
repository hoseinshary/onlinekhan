<template>

  <my-panel>
    <span slot="title">{{modelName}}</span>
    <div slot="body">
      <my-btn-create @click="openModalCreateStore"></my-btn-create>
      <!-- <my-btn-edit @click="showModalEdit"></my-btn-edit>
      <my-btn-delete @click="showModalDelete"></my-btn-delete>-->
      <my-btn-back @click="btnBack"></my-btn-back>
      <br>
      <br>
      <table class="my-table">
        <thead>
          <tr>
            <th>نام</th>
            <th>اولویت</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="row in allObj"
              :key="row.id">
            <td>{{row.Name}}</td>
            <td>{{row.Priority}}</td>
          </tr>
        </tbody>
      </table>

      <modal-create></modal-create>
    </div>
  </my-panel>
</template>

<script>
import { mapState, mapActions } from 'vuex';
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
    ...mapActions('grade', ['openModalCreateStore', 'fillGridStore']),
    btnBack() {
      // var obj = {};
      // this.grade.map12(obj);
      // console.log(obj);

      console.log((1236).format());

      this.$store.dispatch('notify', {
        vm: this,
        body: 'خطا',
        type: 0
      });
    }
  },
  // computed: mapState('grade', ['allObj, modelName']),
  computed: {
    // ...mapState('grade', ['allObj, modelName'])
    //   ...mapState({
    //     allObj: s => s.grade.allObj,
    //     modelName: s => s.grade.modelName
    //   })
    ...mapState('grade', {
      modelName: 'modelName',
      allObj: 'allObj'
    })
  },
  created() {
    this.fillGridStore();
  }
};
</script>

