import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Home from "../views/Home.vue";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/warehouse-balance-report",
    name: "WarehouseBalanceReport",
    component: () =>
      import("../views/warehouse-balance-reports/WarehouseBalanceReport.vue"),
  },
  {
    path: "/move-list",
    name: "MoveList",
    component: () => import("../views/move-list/MoveList.vue"),
  },
  {
    path: "/create-move",
    name: "CreateMove",
    component: () => import("../views/create-move/CreateMove.vue"),
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
