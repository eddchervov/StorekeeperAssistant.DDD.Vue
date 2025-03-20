import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";

Vue.config.productionTip = false;

import vSelect from "vue-select";
import "vue-select/dist/vue-select.css";
Vue.component("VSelect", vSelect);

import VueCtkDateTimePicker from "vue-ctk-date-time-picker";
import "vue-ctk-date-time-picker/dist/vue-ctk-date-time-picker.css";
Vue.component("VueCtkDateTimePicker", VueCtkDateTimePicker);

import Paginate from "vuejs-paginate";
Vue.component("VuePaginate", Paginate);

import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount("#app");
