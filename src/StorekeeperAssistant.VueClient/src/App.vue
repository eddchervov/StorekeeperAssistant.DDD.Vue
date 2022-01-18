<template>
  <div id="app">
    <div class="container">
      <h5 class="mb-3 mt-1">
        <a href="http://eddcher.ru/">Вернуться на eddcher.ru</a>
      </h5>
    </div>
    <nav
      id="nav"
      class="navbar navbar-expand-lg navbar-light bg-light py-2 mb-4"
    >
      <div class="container">
        <router-link to="/" class="navbar-brand">
          Помощник кладовщика
        </router-link>
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarText"
          aria-controls="navbarText"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarText">
          <ul class="navbar-nav me-auto mb-2 mb-lg-0">
            <li class="nav-item">
              <router-link to="/create-move" class="nav-link">
                Создать перемещениe
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/move-list" class="nav-link">
                Список перемещений
              </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/warehouse-balance-report" class="nav-link">
                Отчет остатков склада
              </router-link>
            </li>
          </ul>
        </div>
      </div>
    </nav>

    <div class="container">
      <div class="row">
        <div class="col-12">
          <router-view />
        </div>
      </div>
    </div>

    <footer class="border-top footer text-muted mt-4">
      <div class="container">
        © 2022 - Помощник кладовщика
        <p>
          GitHub:
          <a
            target="_blank"
            href="https://github.com/eddchervov/StorekeeperAssistant.DDD.Vue"
            >eddchervov.StorekeeperAssistant.DDD.Vue</a
          >
        </p>
      </div>
    </footer>
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
  color: #41b883;
  font-weight: 600 !important;
}

.fw-550 {
  font-weight: 550;
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
      someLoadGetInventoryItemsAction: api.GetInventoryItems,
    }),
  },
})
export default class App extends Vue {
  someLoadGetWarehousesAction!: () => any;
  someLoadGetInventoryItemsAction!: () => any;

  mounted(): void {
    this.someLoadGetWarehousesAction;
    this.someLoadGetInventoryItemsAction;
  }
}
</script>
