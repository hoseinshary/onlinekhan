<template>
  <q-page class="flex flex-center">
    <p class="greeting">Hello {{name}}{{exclamationMarks}}</p>
    <q-btn @click="decrement">-</q-btn>
    <q-btn @click="increment">+</q-btn>
    <base-button round></base-button>
    <base-button></base-button>
  </q-page>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import BaseButton from "./BaseButton.vue";
@Component({
  components: {
    BaseButton
  }
})
export default class HelloDecorator extends Vue {
  @Prop() name: string;
  @Prop({ default: 1 }) initialEnthusiasm: number;

  enthusiasm = this.initialEnthusiasm;
  increment() {
    this.enthusiasm++;
  }
  decrement() {
    if (this.enthusiasm > 1) {
      this.enthusiasm--;
    }
  }
  get exclamationMarks(): string {
    return Array(this.enthusiasm + 1).join("!");
  }
}
</script>

<style>
.greeting {
  font-size: 20px;
}
</style>
