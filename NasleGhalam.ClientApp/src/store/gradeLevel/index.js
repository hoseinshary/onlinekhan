export default {
  namespaced: true,
  state: {
    count: 10
  },
  mutations: {
    increment: state => {
      state.count++
      console.log('gradeLevel : ' + state.count)
    },
    decrement: state => state.count--
  },
  actions: {
    increment(context) {
      console.log('action gradelevel increment:')
      context.commit('increment')
    }
  },
  getters: {}
}
