<template>
  

  
   
</template>

<script>

export default {
  props: ["tableData","columns","visibleColumns","customCol","aaa"],
    
  data: () => ({
    myVisibleColumns:[],
    separator: "cell",
    paginationControl:{ rowsPerPage: 10 },
    filter: "",
  }),
  created: function () {
      this.myVisibleColumns =  this.visibleColumns
  },
  components: {
    QTable,
    QTh,
    QTr,
    QTd,
    QTableColumns,
  },

};
</script>

<style scoped>
.q-table-container{
    background: white;
}
</style>
