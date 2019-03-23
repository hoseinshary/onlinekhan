<template>
  <section class="col-sm-6"
           style="height:50px">
    <q-input v-model="key"
             float-label="جستجو"
             clearable
             @input="$emit('onFilterd', key)"
             style="width:200px;" />

  </section>
</template>


<script>
export default {
  props: {
    filterKey: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      key: ''
    };
  },
  watch: {
    filterKey(newVal, oldVal) {
      this.key = newVal;
    }
  }
};
</script>
