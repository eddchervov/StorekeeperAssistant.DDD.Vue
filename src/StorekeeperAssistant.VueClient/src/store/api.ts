const serverPath = "http://api.sa2.eddcher.ru/";

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
  CreateMoving: serverPath + "api/movings",
};
