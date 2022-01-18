import { AddMovingDto } from "@/models/dto/add-moving-dto";
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
   * Получить перемещения
   */
  getMovings: (skipCount: number, takeCount: number) =>
    axios.get(api.GetMovings + "/" + skipCount + "/" + takeCount),
  /**
   * Получить остатки склада
   */
  getWarehouseBalanceReport: (warehouseId: string, date: string | null) =>
    axios.get(
      api.GetWarehouseBalanceReport + "/" + warehouseId + "/" + (date ?? "")
    ),
  /**
   * Создать перемещение
   */
  createMoving: (addMovingDto: AddMovingDto) =>
    axios.post(api.CreateMoving, addMovingDto),
};
