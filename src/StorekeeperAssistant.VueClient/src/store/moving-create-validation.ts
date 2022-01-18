import { InventoryItemVm } from "@/models/view-models/inventory-item-vm";
import { WarehouseInventoryItemVm } from "@/models/view-models/warehouse-inventory-item-vm";

export function validationMoving(
  departureWarehouseInventoryItems: Array<WarehouseInventoryItemVm>
) {
  let countError = 0;
  const messages: Array<string> = [];
  let isNotEmpty = false;
  departureWarehouseInventoryItems.forEach((warehouseInventoryItem) => {
    if (warehouseInventoryItem.newCount) {
      isNotEmpty = true;
      if (warehouseInventoryItem.count < warehouseInventoryItem.newCount) {
        countError++;
        messages.push(
          warehouseInventoryItem.inventoryItem.name +
            ": Вы ввели значения больше чем находится на складе"
        );
      }
    }
  });

  if (isNotEmpty == false) {
    countError++;
    messages.push("Нет добавленных номенклатур");
  }

  return {
    isError: countError != 0,
    countError: countError,
    messages: messages,
  };
}

export function validationComing(
  inventoryItems: Array<InventoryItemVm>,
  maxValueInventoryItem: number
) {
  let countError = 0;
  const messages: Array<string> = [];
  if (inventoryItems.length == 0) {
    countError++;
    messages.push("Нет добавленных номенклатур");
  }
  inventoryItems.forEach((inventoryItem) => {
    if (
      inventoryItem.newCount != null &&
      inventoryItem.newCount > maxValueInventoryItem
    ) {
      countError++;
      messages.push(
        inventoryItem.name +
          " - максимально возможное значение: " +
          maxValueInventoryItem
      );
    } else if (inventoryItem.newCount != null && inventoryItem.newCount < 0) {
      countError++;
      messages.push(
        inventoryItem.name + " - минимально возможное значение: " + 1
      );
    }
  });

  return {
    isError: countError != 0,
    countError: countError,
    messages: messages,
  };
}
