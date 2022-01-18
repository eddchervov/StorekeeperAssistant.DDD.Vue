import { AddMovingDto } from "@/models/dto/add-moving-dto";
import { InventoryItemDto } from "@/models/dto/inventory-item-dto";
import { WarehouseInventoryItemDto } from "@/models/dto/warehouse-inventory-item-dto";
import { InventoryItemVm } from "@/models/view-models/inventory-item-vm";
import { WarehouseInventoryItemVm } from "@/models/view-models/warehouse-inventory-item-vm";
import { Commit } from "vuex";
import client from "./client";
import mutations from "./mutations";

export async function loadInventoryItems(commit: Commit) {
  client
    .getInventoryItems()
    .then((p) => {
      const inventoryItemVms: InventoryItemVm[] = [];
      const inventoryItemDtos = p.data as InventoryItemDto[];
      inventoryItemDtos.forEach((inventoryItem) => {
        inventoryItemVms.push({
          id: inventoryItem.id,
          name: inventoryItem.name,
          newCount: null,
        } as InventoryItemVm);
      });
      commit(mutations.setInventoryItems, inventoryItemVms);
    })
    .catch((e) => {
      commit(mutations.setError, { msg: e });
    });
}

export async function loadWarehouses(commit: Commit) {
  await client
    .getWarehouses()
    .then((p) => {
      commit(mutations.setWarehouses, p.data);
    })
    .catch((e) => {
      commit(mutations.setError, { msg: e });
    });
}

export async function getWarehouseBalanceReport(
  commit: Commit,
  warehouseId: string,
  date: string | null
) {
  await client
    .getWarehouseBalanceReport(warehouseId, date)
    .then((p) => {
      commit(mutations.setWarehouseInventoryItems, p.data);
    })
    .catch((e) => {
      commit(mutations.setError, { msg: e });
    });
}

export async function getMovings(
  commit: Commit,
  skipCount: number,
  takeCount: number
) {
  await client
    .getMovings(skipCount, takeCount)
    .then((p) => {
      commit(mutations.setMovings, p.data);
    })
    .catch((e) => {
      commit(mutations.setError, { msg: e });
    });
}

export function loadDepartureWarehouseInventoryItems(
  commit: Commit,
  warehouseId: string
) {
  client
    .getWarehouseBalanceReport(warehouseId, null)
    .then((p) => {
      commit(mutations.setLoadDepartureWarehouseInventoryItems, false);

      const departureWarehouseInventoryItems: Array<WarehouseInventoryItemVm> =
        [];
      (p.data as Array<WarehouseInventoryItemDto>).forEach(
        (warehouseInventoryItem) => {
          departureWarehouseInventoryItems.push({
            id: warehouseInventoryItem.id,
            count: warehouseInventoryItem.count,
            inventoryItem: warehouseInventoryItem.inventoryItem,
            newCount: null,
          } as WarehouseInventoryItemVm);
        }
      );

      commit(
        mutations.setDepartureWarehouseInventoryItems,
        departureWarehouseInventoryItems
      );
    })
    .catch((e) => {
      commit(mutations.setError, { msg: e });
    });
}

export function saveMoving(commit: Commit, addMovingDto: AddMovingDto) {
  client
    .createMoving(addMovingDto)
    .then((p) => {
      commit(mutations.setData, { isCreateMoving: false });
      location.reload();
    })
    .catch((e) => {
      commit(mutations.setError, { msg: e });
    });
}
