<template>
  <div style="width:600px; margin: 20px auto;">
    <div class="col-6">
      <div style="margin: auto">
        <q-table selection="multiple" :selected.sync="selectedList" title="Table Title" :data="allAccessObj" :columns="mycolumns" row-key="Id" :pagination.sync="paginationControl" :visible-columns="myvisibleColumns" :separator="separator" :filter="filter">
          <template slot="top-left" slot-scope="props">
            <q-search color="secondary" v-model="filter" class="col-12" />
          </template>

          <template slot="top-right" slot-scope="props">
            <q-table-columns color="secondary" class="q-mr-sm" v-model="myvisibleColumns" :columns="mycolumns" />
            <q-select color="secondary" v-model="separator" :options="[
            { label: 'افقی', value: 'horizontal' },
            { label: 'عمودی', value: 'vertical' },
            { label: 'سلول', value: 'cell' },
            { label: 'هیچکدام', value: 'none' }
            ]" hide-underline />
            <q-btn flat round dense :icon="props.inFullscreen ? 'fullscreen_exit' : 'fullscreen'" @click="props.toggleFullscreen" />
          </template>

          <!-- <q-td slot="body-cell-Id" slot-scope="props" :props="props" style="padding-right:5px">
            <q-btn v-if="accessControl.roleAccess" size="sm" round dense color="blue" icon="account_box" @click="loadModalAccessList(props.row.Id,' نقش '+props.row.Name)" class="q-mr-xs">
              <q-tooltip>دسترسی</q-tooltip>
            </q-btn>
            <q-btn v-if="accessControl.canEdit" size="sm" round dense color="blue" icon="edit" @click="loadModalEdit(props.row.Id,' نقش '+props.row.Name)" class="q-mr-xs">
              <q-tooltip>ویرایش</q-tooltip>
            </q-btn>
            <q-btn v-if="accessControl.canDelete" size="sm" round dense color="red" icon="delete_forever" @click="loadModalDelete(props.row.Id,' نقش '+props.row.Name)" class="q-mr-xs">
              <q-tooltip>حذف</q-tooltip>
            </q-btn>
          </q-td> -->

        </q-table>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapActions, mapState, mapMutations } from "vuex";

export default {
  props: {
    allAccessObj: null
  },
  data: () => ({
    selectedList: [],
    selectedListFlag: false,
    mycolumns: [
      {
        name: "ControllerFaName",
        label: "نام منو",
        field: "ControllerFaName",
        align: "left",
        sortable: true
      },
      {
        name: "ActionFaName",
        label: "نام عملیات",
        field: "ActionFaName",
        align: "left",
        sortable: true
      }
    ],
    separator: "cell",
    paginationControl: { rowsPerPage: 10 },
    filter: "",
    myvisibleColumns: ["ControllerFaName", "ActionFaName"]
  }),

  methods: {
    ...mapActions("role", ["changeAccess_Store"])
  },
  created: () => {
    Array.prototype.diff = function(a) {
      return this.filter(function(i) {
        return a.indexOf(i) < 0;
      });
    };
  },
  watch: {
    allAccessObj: function(val, oldVal) {
      this.selectedListFlag = true;
      this.selectedList = this.allAccessObj.filter(x => x.IsChecked);
      setTimeout(() => {
        this.selectedListFlag = false;
      }, 500);
    },
    selectedList: function(newVal, oldVal) {
      if (!this.selectedListFlag) {
        var index = newVal.length;
        var type;
        var list;
        if (newVal.length != 0 || oldVal.length != 0) {
          if (newVal.length > oldVal.length) {
            list = newVal.diff(oldVal).map(x => x.Id);
            type = true;
          } else {
            list = oldVal.diff(newVal).map(x => x.Id);
            type = false;
          }
          debugger;
          this.changeAccess_Store({
            lsit: list,
            ischecked: type
          });
        }
      }
    }
  }
};
</script>