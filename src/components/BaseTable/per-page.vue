<template>
  <section class="col-sm-6">
    <q-select :options='options'
              v-model="model"
              @input="$emit('onPerPageChange', model)"
              style="width:100px; float:left; height:45px;" />
    <label style="float:left;position: relative;top: 20px;">نمایش در صفحه: &nbsp; </label>
  </section>
</template>


<script>
export default {
  props: {
    options: {
      type: Array,
      required: true
    },
    perPage: {
      type: Number,
      required: true
    }
  },
  data() {
    return {
      model: this.perPage
    };
  }
};
</script>
