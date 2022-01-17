import { MovingDto } from "@/models/dto/moving-dto";
import { OptionsDto } from "@/models/dto/options-dto";
import { WarehouseDto } from "@/models/dto/warehouse-dto";
import { WarehouseInventoryItemDto } from "@/models/dto/warehouse-inventory-item-dto";
import { InventoryItemVm } from "@/models/view-models/inventory-item-vm";
import { WarehouseInventoryItemVm } from "@/models/view-models/warehouse-inventory-item-vm";
import Vue from "vue";
import Vuex from "vuex";
import api from "./api";
import {
  loadInventoryItems,
  loadWarehouses,
  getWarehouseBalanceReport,
  getMovings,
  loadDepartureWarehouseInventoryItems,
} from "./client-helper";
import mutations from "./mutations";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    inventoryItems: new Array<InventoryItemVm>(),
    operation: {
      COMING: 1,
      CONSUMPTION: 2,
      MOVING: 3,
    },
    isLoadDepartureWarehouseInventoryItems: true,

    departureWarehouseInventoryItems: new Array<WarehouseInventoryItemVm>(),
    arrivalWarehouseInventoryItems: new Array<WarehouseInventoryItemVm>(),

    warehouses: new Array<WarehouseDto>(),
    departureWarehouses: new Array<WarehouseDto>(),
    arrivalWarehouses: new Array<WarehouseDto>(),

    warehouseInventoryItems: new Array<WarehouseInventoryItemDto>(),

    movingsTotalCount: 0,
    movings: new Array<MovingDto>(),

    serverErrors: new Array<string>(),

    selectDepartureWarehouseId: null,
    selectArrivalWarehouseId: null,

    selectOperation: null,
    typeOperations: [
      { id: 1, name: "Приход" },
      { id: 2, name: "Расход" },
      { id: 3, name: "Перемещение" },
    ] as Array<OptionsDto>,
  },
  getters: {
    departureWarehouseInventoryItems: (state) =>
      state.departureWarehouseInventoryItems as Array<WarehouseInventoryItemVm>,
    arrivalWarehouseInventoryItems: (state) =>
      state.arrivalWarehouseInventoryItems as Array<WarehouseInventoryItemVm>,
    isLoadDepartureWarehouseInventoryItems: (state) =>
      state.isLoadDepartureWarehouseInventoryItems as boolean,
    selectDepartureWarehouseId: (state) =>
      state.selectDepartureWarehouseId as string | null,
    selectArrivalWarehouseId: (state) =>
      state.selectArrivalWarehouseId as string | null,
    inventoryItems: (state) => state.inventoryItems as Array<InventoryItemVm>,
    departureWarehouses: (state) =>
      state.departureWarehouses as Array<WarehouseDto>,
    arrivalWarehouses: (state) =>
      state.arrivalWarehouses as Array<WarehouseDto>,
    warehouses: (state) => state.warehouses as Array<WarehouseDto>,
    warehouseInventoryItems: (state) =>
      state.warehouseInventoryItems as Array<WarehouseInventoryItemDto>,
    movingsTotalCount: (state) => state.movingsTotalCount as number,
    movings: (state) => state.movings as Array<MovingDto>,
    selectOperation: (state) => state.selectOperation as number | null,
    typeOperations: (state) => state.typeOperations as Array<OptionsDto>,
  },
  mutations: {
    [mutations.setDepartureWarehouseInventoryItems]: (state, value) => {
      state.departureWarehouseInventoryItems = value;
    },
    [mutations.setArrivalWarehouseInventoryItems]: (state, value) => {
      state.departureWarehouseInventoryItems = value;
    },
    [mutations.setLoadDepartureWarehouseInventoryItems]: (state, value) => {
      state.isLoadDepartureWarehouseInventoryItems = value;
    },
    [mutations.setDepartureWarehouseId]: (state, value) => {
      state.selectDepartureWarehouseId = value;
    },
    [mutations.setArrivalWarehouseId]: (state, value) => {
      state.selectArrivalWarehouseId = value;
    },
    [mutations.setInventoryItems]: (state, value) => {
      state.inventoryItems = value;
    },
    [mutations.setWarehouses]: (state, value) => {
      state.warehouses = value;
      state.departureWarehouses = value;
      state.arrivalWarehouses = value;
    },
    [mutations.setDepartureWarehouses]: (state, value) => {
      state.departureWarehouses = value;
    },
    [mutations.setArrivalWarehouses]: (state, value) => {
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
    [mutations.changeSelectOperation]: (state, value) => {
      state.selectOperation = value;
      state.selectDepartureWarehouseId = null;
      state.selectArrivalWarehouseId = null;
      state.departureWarehouses = state.warehouses;
      state.arrivalWarehouses = state.warehouses;
    },
  },
  actions: {
    async [api.GetInventoryItems]({ commit }) {
      await loadInventoryItems(commit);
    },
    async [api.GetWarehouses]({ commit }) {
      await loadWarehouses(commit);
    },
    async [api.GetWarehouseBalanceReport]({ commit }, { warehouseId, date }) {
      await getWarehouseBalanceReport(commit, warehouseId, date);
    },
    async [api.GetWarehouseBalanceReport]({ commit }, { warehouseId }) {
      await loadDepartureWarehouseInventoryItems(commit, warehouseId);
    },
    async [api.GetMovings]({ commit }, { skipCount, takeCount }) {
      await getMovings(commit, skipCount, takeCount);
    },
  },
  modules: {},
});
