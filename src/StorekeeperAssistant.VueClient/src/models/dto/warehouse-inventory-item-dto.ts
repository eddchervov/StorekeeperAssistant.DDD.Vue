import { InventoryItemDto } from "./inventory-item-dto";

export interface WarehouseInventoryItemDto {
  id: string;
  count: number;
  inventoryItem: InventoryItemDto;
}
