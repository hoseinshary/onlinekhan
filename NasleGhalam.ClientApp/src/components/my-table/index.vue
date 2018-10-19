<!--<template>
   <section>
    <slot></slot>
    <table>
      <thead>
        <tr>
          <th v-for="col in columns" @click="sortBy(col)" :key="col.key" :class="{ active: (col.sortable && sortKey == col.data) }">
            {{ col.title }}
            <span v-if="col.sortable" class="arrow" :class="sortOrders[col.data] > 0 ? 'asc' : 'dsc'">
            </span>
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="row in filteredData" :key="row.id">
          <template v-for="(col, index) in columns">
            <td v-if="col.render" :key="col.name" v-html="col.render(row[col.data], 'display', row, index)"></td>
            <td v-else :key="col.name">
              {{row[col.data]}}
            </td>
          </template>
        </tr>
      </tbody>
    </table>
  </section> 
 </template>-->

<script>
import util from 'utilities/util';
import filterKey from './filter-key.vue';
import perPage from './per-page.vue';
import pagination from './pagination.vue';
import tableSummary from './table-summary.vue';

export default {
  components: {
    filterKey,
    perPage,
    pagination,
    tableSummary
  },
  render(h) {
    var tbl = this;
    // var $slots = tbl.$slots;
    var $scopedSlots = tbl.$scopedSlots;

    var icon_asc = h('q-icon', {
      props: {
        name: 'arrow_drop_up',
        color: 'red',
        size: '18px'
      }
    });

    var icon_desc = h('q-icon', {
      props: {
        name: 'arrow_drop_down',
        color: 'red',
        size: '18px'
      }
    });

    // create th of columns
    var column = tbl.columns.map(function(col, index) {
      // return if col is hidden
      if (!col.visible) return;

      var data = {
        key: col.data + '_' + index,
        // class: {
        //   active: col.sortable && tbl.sortKey == col.data
        // },
        on: {
          click: function click(evt) {
            evt.stopPropagation();
            evt.preventDefault();
            tbl.sortBy(col);
          }
          // keydown: function keydown(evt) {
          //   var keyCode = evt.keyCode;
          //   if (keyCode === KeyCodes.ENTER || keyCode === KeyCodes.SPACE) {
          //     evt.stopPropagation();
          //     evt.preventDefault();
          //     t.headClicked(evt, field);
          //   }
          // }
        }
      };

      let slot = $scopedSlots['HEAD_' + col.data];
      let child = [];
      if (slot) {
        child.push(slot({ title: col.title, col: col }));
      } else {
        child.push(h('span', col.title));
      }

      if (col.sortable && tbl.sortKey == col.data) {
        child.unshift(tbl.sortOrders[col.data] == 1 ? icon_asc : icon_desc);
      }
      return h('th', data, child);
    });

    // create thead
    var thead = h('thead', [h('tr', column)]);
    //--------------------------------------------

    //create row data
    var rows = [];
    tbl.filteredData.forEach(function(row, rowIndex) {
      // set index of current row
      if (tbl.hasIndex) {
        row['_index'] = rowIndex + tbl.rowFrom;
      }

      let tds = [];
      tds.push(
        tbl.columns.map(function(col, colIndex) {
          // return if col is hidden
          if (!col.visible) return;
          let colData = {
            key: 'row_' + rowIndex + '_cell_' + colIndex
          };

          let slot = $scopedSlots[col.data];
          if (slot) {
            slot = [
              slot({
                value: row[col.data],
                row: row,
                index: rowIndex
              })
            ];
          } else {
            colData.domProps = {
              innerHTML: col.render
                ? col.render(row[col.data], 'display', row, rowIndex)
                : row[col.data]
            };
          }

          return h('td', colData, slot);
        })
      );
      //---------------------
      let rowData = {
        key: 'row_' + rowIndex,
        class: {
          selected: row._rowSelected
        },
        on: {
          click: function click(e) {
            tbl.rowClicked(e, row, rowIndex);
          }
        }
      };

      rows.push(h('tr', rowData, tds));
    });

    var tbody = h('tbody', rows);

    var table = h(
      'table',
      {
        class: {
          'my-table': true
        }
      },
      [thead, tbody]
    );

    var tableContainerRender = h(
      'div',
      {
        attrs: {
          class: 'col-12'
        }
      },
      [table]
    );

    //  filter key
    let searchKeyContainerRender = h('filter-key', {
      props: {
        filterKey: tbl.filterKey
      },
      on: {
        onFilterd(val) {
          tbl.filterKey = val;
        }
      }
    });
    //  filter key------------------------------------------------------------------

    // per page
    let perPageContainerRender = h('per-page', {
      props: {
        options: tbl.pageOptions,
        perPage: tbl.perPage
      },
      on: {
        onPerPageChange(val) {
          tbl.perPage = val;
        }
      }
    });
    //  per page------------------------------------------------------------------

    // pagination
    let paginationContainerRender = h('pagination', {
      props: {
        totalRows: tbl.filterRows,
        perPage: tbl.perPage,
        currentPage: tbl.currentPage
      },
      //   domProps: {
      //     currentPage: tbl.currentPage
      //   },
      on: {
        onPaginationChange(val) {
          tbl.currentPage = val;
        }
      }
    });
    // pagination------------------------------------------------------------------

    // summary
    let summaryContainerRender = h('table-summary', {
      props: {
        totalRows: tbl.gridData.length,
        filterRows: tbl.filterRows,
        rowTo: tbl.rowTo,
        rowFrom: tbl.rowFrom
      }
    });
    // summary------------------------------------------------------------------

    return h(
      'div',
      {
        attrs: {
          class: 'row col-xs-12'
        }
      },
      [
        searchKeyContainerRender,
        perPageContainerRender,
        tableContainerRender,
        paginationContainerRender,
        summaryContainerRender
      ]
    );
  },

  props: {
    gridData: {
      type: Array,
      required: true
    },
    columns: {
      type: Array,
      required: true
    },
    cssClass: {
      type: String,
      required: false,
      default: ''
    },
    hasIndex: {
      type: Boolean,
      default: false
    }
  },
  data: function() {
    // init columns
    this.initColumn();

    // init sort order
    var sortOrders = {};
    this.columns.forEach(function(col) {
      sortOrders[col.data] = 1;
    });
    let gridDataRows = this.gridData.length;
    let pageOptions = [
      {
        label: '10',
        value: 10
      },
      {
        label: '25',
        value: 25
      },
      {
        label: '50',
        value: 50
      },
      {
        label: '100',
        value: 100
      },
      {
        label: 'همه',
        value: 'همه'
      }
    ]; //[10, 25, 50, 100, 'همه'];

    return {
      filterKey: '',
      currentPage: 1,
      perPage: pageOptions[0].value,
      pageOptions: pageOptions,
      // totalRows: gridDataRows,
      filterRows: gridDataRows,
      sortKey: '',
      sortOrders: sortOrders,
      rowClickCount: 0
    };
  },

  computed: {
    /**
     *  computed: search and sort gridData
     * @returns {[object[]]}
     */
    filteredData() {
      var sortKey = this.sortKey;
      var filterKey = this.filterKey && this.filterKey.toLowerCase();
      var order = this.sortOrders[sortKey] || 1;
      var gridData = this.gridData;
      this.rowClickCount;

      let renderType = '';
      //debugger; // this or not
      // set renderType
      if (sortKey || filterKey) {
        renderType = 'sortOrSearch';
      } else {
        renderType = 'display';
      }

      if (filterKey) {
        // iterate in grid data to filter
        gridData = gridData.filter((row, index) => {
          return this.columns.some(col => {
            // if col isn't searchable then continue
            if (!col.searchable) return false;

            var result = false;
            if (col.render) {
              // get value of col function
              result =
                String(col.render(row[col.data], renderType, row, index))
                  .toLowerCase()
                  .indexOf(filterKey) > -1;
            } else {
              // get value of col
              //todo: check later. use slot if posible
              result =
                // String(row[col.data])
                String(util.getNested(row, col.data))
                  .toLowerCase()
                  .indexOf(filterKey) > -1;
            }
            return result;
          });
        });
      }
      debugger;

      if (sortKey) {
        // gridData = gridData.slice().sort(function(a, b) {
        gridData = gridData.sort(function(a, b) {
          //todo: check later. use slot if posible
          a = util.getNested(a, sortKey);
          b = util.getNested(b, sortKey);
          // a = a[sortKey];
          // b = b[sortKey];
          return (a === b ? 0 : a > b ? 1 : -1) * order;
        });
      }

      /* eslint-disable */
      //debugger;
      this.filterRows = gridData.length;

      // set perPage to total
      if (this.perPage == 'همه') {
        this.perPage = this.gridData.length;
      }

      // Grab the current page of data (which may be past filtered items)
      gridData = gridData.slice(
        (this.currentPage - 1) * this.perPage,
        this.currentPage * this.perPage
      );
      return gridData;
    },

    /**
     * computed: compute row from
     * @returns Number
     */
    rowFrom() {
      return this.currentPage * this.perPage - (this.perPage - 1);
    },

    /**
     * computed: compute row to
     * @returns Number
     */
    rowTo() {
      let to = this.currentPage * this.perPage;
      if (to <= this.filterRows) return to;
      return this.filterRows;
    }
  },
  // filters: {
  //   capitalize: function(str) {
  //     return str.charAt(0).toUpperCase() + str.slice(1);
  //   }
  // },

  methods: {
    /**
     * method: init columns with default
     * @returns {void}
     */
    initColumn() {
      //
      const len = this.columns.length;
      for (let i = 0; i < len; i++) {
        // loop in items
        var item = this.columns[i];
        if (util.isObject(item)) {
          // if item is object then check prop
          if (!item.data) {
            throw "property 'data' of columns doesn't set";
          }
          if (!item.title) {
            item.title = item.data;
          }

          item.searchable = item.searchable == undefined || item.searchable;
          item.sortable = item.sortable == undefined || item.sortable;
          item.visible = item.visible == undefined || item.visible;
          item.key = i;
        } else if (util.isString(item)) {
          // if item is string then convert to object
          this.columns[i] = {
            title: item,
            data: item,
            searchable: true,
            sortable: true,
            visible: true,
            key: i
          };
        } else {
          throw 'columns must be array of string or object';
        }
      }

      if (
        this.hasIndex &&
        !this.columns.some(val => {
          return val.data == '_index';
        })
      ) {
        this.columns.unshift({
          title: 'ردیف',
          data: '_index',
          searchable: false,
          sortable: false,
          visible: true,
          key: 0
        });
      }
    },
    /**
     * method: sort gridData
     * @param {Object} col
     * @param {Boolean} col.sortable
     * @param {String} col.data
     */
    sortBy(col) {
      if (!col.sortable) return;

      this.sortKey = col.data;
      this.sortOrders[col.data] = this.sortOrders[col.data] * -1;
      this.currentPage = 1;
    },
    /** row clicked event
     * @param {Event} event
     * @param {[]} row
     * @param {Number} index
     */
    rowClicked(e, row, index) {
      this.gridData.forEach(function(currentRow) {
        currentRow._rowSelected = false;
      });
      row._rowSelected = true;

      // trigger computed filterdData
      this.rowClickCount++;
    }
  }
};
</script>

<style>
.my-table {
  width: 100% !important;
}

.my-table tr td {
  vertical-align: middle;
  text-align: center;
  border: 1px solid rgb(230, 230, 230);
  height: 30px;
}

.my-table th {
  background-color: #e6e6e6;
  color: #524f4f;
  vertical-align: middle;
  text-align: center;
  font-size: 13px;
}

.my-table tr:nth-child(2n + 2) {
  background-color: #f3f3f3;
}

.my-table tr:nth-child(2n + 1) {
  background-color: #fafafa;
}

/* .my-table tr td table tr td {
  width: 22px;
  font-weight: bold;
} */

table.my-table tbody tr.selected {
  background-color: #b0bed9 !important;
  transition: all 400ms linear; /*cubic-bezier(1, 0, 1, 0); */
  color: #c50505;
  font-weight: bold;
  /* background-color: purple; */
}

/* table.my-table thead th.active {
  color: #c50505;
} */
</style>

