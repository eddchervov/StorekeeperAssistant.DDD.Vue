import { Commit } from "vuex";
import client from "./client";
import mutations from "./mutations";

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
