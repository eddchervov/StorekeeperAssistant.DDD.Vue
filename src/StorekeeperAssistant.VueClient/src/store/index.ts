import { MovingDto } from "@/models/dto/moving-dto";
import { WarehouseDto } from "@/models/dto/warehouse-dto";
import { WarehouseInventoryItemDto } from "@/models/dto/warehouse-inventory-item-dto";
import Vue from "vue";
import Vuex from "vuex";
import api from "./api";
import {
  loadWarehouses,
  getWarehouseBalanceReport,
  getMovings,
} from "./data-loader";
import mutations from "./mutations";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    warehouses: new Array<WarehouseDto>(),
    departureWarehouses: new Array<WarehouseDto>(),
    arrivalWarehouses: new Array<WarehouseDto>(),

    warehouseInventoryItems: new Array<WarehouseInventoryItemDto>(),

    movingsTotalCount: 0,
    movings: new Array<MovingDto>(),

    serverErrors: new Array<string>()
  },
  getters: {
    warehouses: (state) => state.warehouses,
    warehouseInventoryItems: (state) => state.warehouseInventoryItems,
    movingsTotalCount: (state) => state.movingsTotalCount,
    movings: (state) => state.movings,
  },
  mutations: {
    [mutations.setWarehouses]: (state, value) => {
      state.warehouses = value;
      state.departureWarehouses = value;
      state.arrivalWarehouses = value;
    },
    [mutations.setWarehouseInventoryItems]: (state, value) => {
      state.warehouseInventoryItems = value;
    },
    [mutations.setMovings]: (state, value) => {
      state.movingsTotalCount = value.totalCount;
      state.movings = value.movings;
    },
    [mutations.setError]: (state, { msg }) => {
      if (state.serverErrors.length > 2) state.serverErrors.splice(0, 1);
      state.serverErrors.push(msg);
      setTimeout(() => {
        const index = state.serverErrors.indexOf(msg);
        state.serverErrors.splice(index, 1);
      }, 3000);
    },
  },
  actions: {
    async [api.GetWarehouses]({ commit }) {
      await loadWarehouses(commit);
    },
    async [api.GetWarehouseBalanceReport]({ commit }, { warehouseId, date }) {
      await getWarehouseBalanceReport(commit, warehouseId, date);
    },
    async [api.GetMovings]({ commit }, { skipCount, takeCount }) {
      await getMovings(commit, skipCount, takeCount);
    },
  },
  modules: {},
});
