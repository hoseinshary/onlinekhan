import eventhub from "eventhub";
// import Vue from "Vue";
// console.log("a");
// export const eventh = new Vue();
export default ({ Vue }) => {
  Vue.use(eventhub);
  window.$eventHub = eventhub;
};
