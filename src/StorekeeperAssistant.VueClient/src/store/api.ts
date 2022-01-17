const serverPath = "http://localhost:45210/";

export default {
  /**
   * Получить склады
   */
  GetWarehouses: serverPath + "api/warehouses",

  /**
   * Получить номенклатуры
   */
  GetInventoryItems: serverPath + "/inventory-items/get",

  /**
   * Получить перемещения
   */
  GetMovings: serverPath + "api/movings",

  /**
   * Получить остатки склада
   */
  GetWarehouseBalanceReport: serverPath + "api/warehouse-balance-report",

  /**
   * Создать перемещение
   */
  CreateMoving: serverPath + "/movings/create",
};
