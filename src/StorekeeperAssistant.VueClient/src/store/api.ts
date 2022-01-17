const serverPath = "http://localhost:45210/";

export default {
  /**
   * Получить склады
   */
  GetWarehouses: serverPath + "api/warehouses",

  /**
   * Получить номенклатуры
   */
  GetInventoryItems: serverPath + "api/inventory-items",

  /**
   * Получить перемещения
   */
  GetMovings: serverPath + "api/movings",

  /**
   * Получить отчет остатков склада
   */
  GetWarehouseBalanceReport: serverPath + "api/warehouse-balance-report",

  /**
   * Получить остатки склада
   */
  WarehouseInventoryItems: serverPath + "api/warehouse-balance-report",

  /**
   * Создать перемещение
   */
  CreateMoving: serverPath + "/movings/create",
};
