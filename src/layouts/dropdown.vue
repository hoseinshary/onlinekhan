<template>
  <div class="subnav" >
    <div class="subnavbtn" 
    @click="clickSubnav(id)"
    >
      <span>{{ title }}</span>
      <q-icon name="expand_more" class="q-ml-sm"/>
    </div>
    
    <transition name="fade" appear>
      <div class="subnav-content" v-if="isOpen">
        <div v-for="(item, i) in items" :key="i" class="column">
          <router-link :to="item.EnName">{{ item.FaName }}</router-link>
        </div>
      </div>
    </transition>
  </div>
</template>

<script>
export default {
  name: 'dropdown',
  props: ['title', 'items','id','isOpen'],
  emits:['toggleSubnav'],
  data () {
    return {
      isopen: this.isOpen
    }
  },
  methods:{
    clickSubnav(id){
      this.$emit('toggleSubnav',id);
    }
  }
}
</script>

<style>

.fade-enter-active,
.fade-leave-active {
  transition: all .5s ease-out;
}
.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>