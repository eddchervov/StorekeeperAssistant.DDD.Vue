import axios from "axios";
import api from "./api";

/**
 * Клиент взаимодействия с сервером
 */
export default {
  /**
   * Получить склады
   */
  getWarehouses: () => axios.get(api.GetWarehouses),
  /**
   * Получить номенклатуры
   */
  getInventoryItems: () => axios.get(api.GetInventoryItems),
  /**
   * Получить остатки склада
   */
  getWarehouseBalanceReport: (warehouseId: string, date: string | null) =>
    axios.get(api.GetWarehouseBalanceReport + "/" + warehouseId + "/" + (date ?? '')),
  /**
   * Создать перемещение
   */
  createMoving: (data) => axios.post(api.CreateMoving, data),
};
