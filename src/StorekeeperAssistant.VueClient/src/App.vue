
<template>
  <div id="app">
    <div id="nav" class="text-center">
      <router-link to="/">Home</router-link> |
      <router-link to="/about">About</router-link> |
      <router-link to="/warehouse-balance-report">
        Отчет остатков склада
      </router-link>
    </div>
    <div class="container">
      <div class="row">
        <div class="col-12">
          <router-view />
        </div>
      </div>
    </div>
  </div>
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { mapActions } from "vuex";
import api from "./store/api";

@Component({
  computed: {
    ...mapActions({
      someLoadGetWarehousesAction: api.GetWarehouses,
    }),
  },
})
export default class App extends Vue {
  someLoadGetWarehousesAction!: () => any;

  mounted(): void {
    this.someLoadGetWarehousesAction;
  }
}
</script>
