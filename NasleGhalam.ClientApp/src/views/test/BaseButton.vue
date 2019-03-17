<template>
  <q-btn
    outline
    icon="create"
    color="purple"
    size="sm"
    class="shadow-1 bg-white q-mr-sm"
    @click="increment"
    :disabled="disabled"
  >{{count}}ویرایش</q-btn>
</template>

<script lang="ts">
import { Vue, Component, Prop, Watch } from "vue-property-decorator";
@Component({
  props: {
    //   round: Boolean,
    disabled: {
      default: false
    }
  }
  // watch: {
  //   count(val, oldVal) {
  //     console.log("watch " + val);
  //   }
  // }
})
export default class BaseButton extends Vue {
  @Prop({ default: false }) round: boolean;
  count: number = 10;
  increment() {
    this.count++;
  }

  @Watch("count")
  countWhatch(val, oldVal) {
    console.log("watch " + val);
  }

  created() {
    console.log(this.round);
    console.log(this.$props.disabled);
  }
}
</script>

<style>
</style>

