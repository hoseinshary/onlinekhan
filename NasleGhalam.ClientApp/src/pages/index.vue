<template>
  <q-page class="flex flex-center">
    <img alt="Quasar logo" src="~assets/quasar-logo-full.svg">
    <button @click="convert">click me</button>
  </q-page>
</template>

<style>
</style>

<script>


export default {
  name: 'PageIndex',
  data:()=>({
    data:[]
  }),methods:{
    convert: function() {
      debugger
      var end=[];
      this.data.forEach(element => {
      var row ="[";
        element.split(' ').forEach(item => {
          var arr = item.split(',')
             row += '{ "lat": ' + arr[1] +', "lng": ' + arr[0] + '},'
        });
        row += "]"
        end.push(row);
      });
        console.log(end)
      
    },
  }
}
</script>
